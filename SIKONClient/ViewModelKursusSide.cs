﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Calls;
using Windows.Security.Authentication.Web.Provider;
using SIKONClassLibrary;
using SIKONClassLibrary.EventHandlers;
using SIKONClient.Annotations;
using SIKONClient.Common;
using SIKONClient.Model;

namespace SIKONClient
{
    class ViewModelKursusSide : INotifyPropertyChanged
    {

        private ICommand _tilmeldCommand;
        private ICommand _sendCommand;
        private AccountToEvent AccountObj;
        private string _knaptekst;
       private string _availabilityText;
       private string _color;


       public Singleton SikonSingleton { get; set; }

        public ObservableCollection<Question> QuestionList { get; set; } // vi opretter en reference til vores OC af question

        public Question UserQuestion { get; set; }

        public ICommand TilmeldCommand { get; set; }

        public ICommand SendCommand { get; set; }

        public string Color
        {
            get => _color;
            set { _color = value; OnPropertyChanged("Color");}
        }

        public string Knaptekst
        {
            get => _knaptekst;
            set { _knaptekst = value; OnPropertyChanged("Knaptekst");  
            }
        }

        public string AvailabilityText
        {
            get => _availabilityText;
            set { _availabilityText = value; OnPropertyChanged("AvailabilityText"); } 
        }

        public Room eventRoom { get; set; }


        public ViewModelKursusSide()
        {

            TilmeldCommand = new RelayCommand(AddEventToAccount);

            SendCommand = new RelayCommand(AddQuestionToEvent);

            SikonSingleton = Singleton.Instance;

            Knaptekst = "Tilmeld";
            AvailabilityText = "Ledig pladser";

            FindAccountInEvent();

            AvailabilityTjek();


            if (SikonSingleton.SelectedEvent.Room_ID == null)
            {
                AvailabilityText = "Der er ikke tilføjet et lokale";
            }
            else
            {

                int i = SikonSingleton.SelectedEvent.Room_ID ?? default(int);
                eventRoom = new RoomHandler().ReadFrom(i);


            }
        }

        private void AddEventToAccount()
        {
            // OBS MANGLER AT IMPLEMENTERE HÅNDTERERING AF "OVERBOOKING"


            if (AccountObj == null)
            {
                AccountObj = new AccountToEvent();
                AccountObj.Event_ID = SikonSingleton.SelectedEvent.ID;
                AccountObj.Account_ID = SikonSingleton.LoggedAccount.Email;
                new AccountToEventHandler().Create(AccountObj);
                Knaptekst = "Afmeld";
                
                FindAccountInEvent();

            }
            else
            {
                new AccountToEventHandler().Delete(AccountObj.ID);
                Knaptekst = "Tilmeld";
                AccountObj = null;
            }

        }

        private void AvailabilityTjek()
        {
            List<AccountToEvent> alList = new AccountToEventHandler().Read();
            List<AccountToEvent> antaList = new List<AccountToEvent>();
            foreach (var a in alList.Where(a => SikonSingleton.SelectedEvent.ID == a.Event_ID))
            {
                antaList.Add(a);

            }

            if (SikonSingleton.SelectedEvent.Room_ID == null)
            {
                AvailabilityText = "Der er ikke tilføjet et lokale";
            }
            else
            {

                int i = SikonSingleton.SelectedEvent.Room_ID ?? default(int);
                Room eventRoom = new RoomHandler().ReadFrom(i);

                //AvailabilityText = $"{eventRoom.Capacity}, Pladser ialt {antaList.Count}";


                if (antaList.Count < (eventRoom.Capacity * 0.8))
                {
                    AvailabilityText = "Ledig pladser";
                    Color = "Green";
                }
                else if (antaList.Count == eventRoom.Capacity)
                {
                    AvailabilityText = "Fuld Booket";
                    Color = "Red";
                }
                else
                {
                    AvailabilityText = "Få ledige pladser";
                    Color = "Yellow";
                }
            }
        }

        private void FindAccountInEvent()
        {
            List<AccountToEvent> AteList = new AccountToEventHandler().Read();

            foreach (var e in AteList)
            {
                if ((e.Event_ID == SikonSingleton.SelectedEvent.ID) && (e.Account_ID == SikonSingleton.LoggedAccount.Email))
                {
                    AccountObj = e;
                    Knaptekst = "Afmeld";
                }

            }
        }

       private void AddQuestionToEvent()
       {
           List<Question> liste = new QuestionHandler().Read();

           QuestionList = new ObservableCollection<Question>();
           foreach (var q in liste.Where(q => q.Event_ID == SikonSingleton.SelectedEvent.ID))
           {
               QuestionList.Add(q);
           }

           UserQuestion = new Question();

       }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
