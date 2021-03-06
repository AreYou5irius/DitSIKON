﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Calls;
using Windows.Security.Authentication.Web.Provider;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using SIKONClassLibrary;
using SIKONClassLibrary.EventHandlers;
using SIKONClient.Annotations;
using SIKONClient.Common;
using SIKONClient.Model;

namespace SIKONClient.ViewModel
{
    class ViewModelKursusSide : INotifyPropertyChanged
    {
        private string _knaptekst;
        private string _availabilityText; 
        private string _color;
        private string _tilmeldColor;

        public string SubjectT { get; set; }
        public string DescriptionT { get; set; }

        private AccountToEvent AccountObj;
        private Question _questionObj;
        public Room SelectedRoom { get; set; }
        public Singleton SikonSingleton { get; set; }
        public Event SelectedEvent { get; set; }
        public Room eventRoom { get; set; }
        public TimeToEvent Time { get; set; }

        public ObservableCollection<Question> QuestionList { get; set; }
        public ObservableCollection<Account> MyAccountList { get; set; }
        public ObservableCollection<Room> RoomList { get; set; }

        public ICommand TilmeldCommand { get; set; }
        public ICommand SendCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateEventCommand { get; set; }


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

        public string TilmeldColor
        {
            get => _tilmeldColor;
            set { _tilmeldColor = value; OnPropertyChanged("TilmeldColor");}
        }

        public ViewModelKursusSide()
        {
            SikonSingleton = Singleton.Instance;
            SelectedEvent = SikonSingleton.SelectedEvent;
            RoomList = new ObservableCollection<Room>(new RoomHandler().Read());

            TilmeldCommand = new RelayCommand(AddEventToAccount);
            SendCommand = new RelayCommand(AddQuestionToEvent);
            DeleteCommand = new RelayCommand(DeleteEvent);
            UpdateEventCommand = new RelayCommand(UpdateEvent);


            Knaptekst = "Tilmeld";
            TilmeldColor = "Green";
            AvailabilityText = "Ledig pladser";
            
            _questionObj = new Question();

            FindAccountInEvent();
            AvailabilityTjek();
            AccountsAddedToEvent();
            QuestionsAddedToEventList();
        
            if (SikonSingleton.SelectedEvent.Room_ID == null)
            {
                AvailabilityText = "Der er ikke tilføjet et lokale";
                
            }
            else
            {
                int i = SikonSingleton.SelectedEvent.Room_ID ?? default(int);
                eventRoom = new RoomHandler().ReadFrom(i);
            }

            List<TimeToEvent> timeToEvents = new TimeToEventHandler().Read();
            foreach (var tte in timeToEvents)
            {
                if (SikonSingleton.SelectedEvent.ID == tte.Event_ID)
                {
                    Time = tte;
                }
            }
        }

        /// <summary>
        /// Tager QuestionList opretter en ny version af denne og filtrere den hvor Event_ID på Question og ID på SelectedEvent matcher
        /// </summary>
        private void QuestionsAddedToEventList() 
        {
            QuestionList = new ObservableCollection<Question>();

            List<Question> qList = new QuestionHandler().Read();
            foreach (var q in qList.Where(q =>q.Event_ID == SikonSingleton.SelectedEvent.ID))
            {
                QuestionList.Add(q);
            }
        }

        /// <summary>
        /// Opretter en ny list med Accounts der er tilmeldt et givent event. Dette er gjort med foreach'es og if's
        /// </summary>
        private void AccountsAddedToEvent()
        {
            List<Account> AccountList = new AccountHandler().Read();

            MyAccountList = new ObservableCollection<Account>();

            List<AccountToEvent> TilmeldteAccounts = new AccountToEventHandler().Read();
            List<AccountToEvent> list = new List<AccountToEvent>();
            foreach (var f in TilmeldteAccounts)
            {
                if (f.Event_ID == SikonSingleton.SelectedEvent.ID)
                {
                    list.Add(f);
                }
            }

            foreach (var ta in list)
            {
                foreach (var a in AccountList)
                {
                    if (ta.Account_ID == a.Email)
                    {
                        MyAccountList.Add(a);
                    }
                }
            }


        }


        /// <summary>
        /// Tjekker om der er plads til flere tilmeldinger og tilmelder LoggedAccount til hvis tjekket succeder
        /// </summary>
        private async void AddEventToAccount()
        {
            List<AccountToEvent> tilmeldte = AvailabilityTjek();
            int i = SikonSingleton.SelectedEvent.Room_ID ?? default(int);

            FindAccountInEvent();

            try
            {
                if (AccountObj == null)
                {
                    if (tilmeldte.Count >= eventRoom.Capacity)
                    {
                        throw new Exception("Kurset er fuldt booket!");
                    }
                }

                if (SikonSingleton.SelectedEvent.Room_ID == null)
                {
                    throw new Exception("Event har intet tilknyttet lokale");
                }

                if (SikonSingleton.LoggedAccount != null)
                {
                    ContentDialog dialog = new ContentDialog();

                    if (AccountObj == null)
                    {

                        AccountObj = new AccountToEvent();
                        AccountObj.Event_ID = SikonSingleton.SelectedEvent.ID;
                        AccountObj.Account_ID = SikonSingleton.LoggedAccount.Email;
                        new AccountToEventHandler().Create(AccountObj);
                        Knaptekst = "Afmeld";
                        TilmeldColor = "Red";

                        FindAccountInEvent();

                        
                        dialog.Content = "Du er nu tilmeldt!";
                        dialog.CloseButtonText = "Ok";
                        dialog.ShowAsync();
                    }
                    else
                    {
                        new AccountToEventHandler().Delete(AccountObj.ID);
                        Knaptekst = "Tilmeld";
                        TilmeldColor = "Green";
                        AccountObj = null;
                        dialog.Content = "Du er nu Afmeldt!";
                        dialog.CloseButtonText = "Ok";
                        dialog.ShowAsync();
                    }
                    AvailabilityTjek();
                }
            }
            catch (Exception e)
            {
                ContentDialog dialog = new ContentDialog(){Content = e.Message, CloseButtonText = "Ok"};
                dialog.ShowAsync();
                
            }
        }

        /// <summary>
        /// Tjekker hvor mange der er tilmeldt et givent kursus
        /// </summary>
        /// <returns>Returnere Deltagerlisten på det givne kursus</returns>
        private List<AccountToEvent> AvailabilityTjek()
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

            return antaList;
        }

        /// <summary>
        /// Tjekker om brugeren er tilmeldt kurset
        /// </summary>
        private void FindAccountInEvent()
        {
            List<AccountToEvent> AteList = new AccountToEventHandler().Read();

            foreach (var e in AteList)
            {
                try
                {
                    if ((e.Event_ID == SikonSingleton.SelectedEvent.ID) && (e.Account_ID == SikonSingleton.LoggedAccount.Email))
                    {
                        AccountObj = e;
                        Knaptekst = "Afmeld";
                        TilmeldColor = "Red";
                    }
                }
                catch (Exception exception)
                {
                    Knaptekst = "Ikke Logget ind";
                    TilmeldColor = "Grey";
                }
            }
        }

        /// <summary>
        /// Tilføjer spørgsmål til Databasen
        /// </summary>
        private async void AddQuestionToEvent() 
        {
           _questionObj.Description = DescriptionT;
           _questionObj.Subject = SubjectT;
           _questionObj.Event_ID = SikonSingleton.SelectedEvent.ID;
           _questionObj.Account_ID = SikonSingleton.LoggedAccount.Email;

           new QuestionHandler().Create(_questionObj);

           ContentDialog dialog = new ContentDialog(){Content = "Du har nu tilføjet et spørgsmål til kurset!", CloseButtonText  = "Ok"};
           dialog.ShowAsync();
        }

        /// <summary>
        /// Laver en pop up der spørger om brugeren ønsker at slette eller om de fortryder at de trykkede "slet"
        /// den kalder så funktionen CommandInvokedHandler der sletter hvis vi trykker "slet kursus"
        /// vi har sat default svar er slet kursus og bliver aktiveret ved "enter"
        /// den kører Async så applikationen kører i baggrunden men pop up'en venter på svar.
        /// </summary>
        private async void  DeleteEvent()
        {
            ContentDialog dialog = new ContentDialog()
            {
                Content = "Er du sikker på du ønsker at slette Kurset?", 
                PrimaryButtonText = "Ja, Slet",
                CloseButtonText = "Fortryd",
             
            };
            ContentDialogResult result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                new EventsHandler().Delete(SelectedEvent.ID);
            }
            //BUG Timing problemer kører først denne funktion efter constructoren er kørt på kurser viewet
        }
        
        /// <summary>
        /// Opdaterer event information. Denne funktion bliver kun brugt ifm. ændring af rum. 
        /// </summary>
        private void UpdateEvent()
        {
            SikonSingleton.SelectedEvent.Room_ID = SelectedRoom.ID;
            SikonSingleton.SelectedEvent.Room = null;
            new EventsHandler().Update(SikonSingleton.SelectedEvent, SikonSingleton.SelectedEvent.ID);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}