using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Calls;
using Windows.Security.Authentication.Web.Provider;
using SIKONClassLibrary;
using SIKONClassLibrary.EventHandlers;
using SIKONClient.Common;
using SIKONClient.Model;

namespace SIKONClient
{
    class ViewModelKursusSide
    {

        private ICommand _tilmeldCommand;
        private ICommand _sendCommand;
        private AccountToEvent AccountObj;

        public Singleton SikonSingleton { get; set; }

        public ObservableCollection<Question> QuestionList { get; set; } // vi opretter en reference til vores OC af question

        public Question UserQuestion { get; set; }

        public ICommand TilmeldCommand { get; set; }

        public ICommand SendCommand { get; set; }

        public string Knaptekst { get; set; }

       

        public ViewModelKursusSide()
        {

            TilmeldCommand = new RelayCommand(AddEventToAccount);

            SendCommand = new RelayCommand(AddQuestionToEvent);

            SikonSingleton = Singleton.Instance;

            Knaptekst = "Tilmeld";

            List<AccountToEvent> AteList = new AccountToEventHandler().Read();
            AccountObj = new AccountToEvent();
            foreach (var e in AteList)
            {
                if ((e.Event_ID == SikonSingleton.SelectedEvent.ID) && (e.Account_ID == SikonSingleton.LoggedAccount.Email))
                {
                    AccountObj = e;
                    Knaptekst = "Afmeld";
                }


            }

        }

        private void AddEventToAccount()
        {

            if (AccountObj == null)
            {
                AccountObj.Event_ID = SikonSingleton.SelectedEvent.ID;
                AccountObj.Account_ID = SikonSingleton.LoggedAccount.Email;
                new AccountToEventHandler().Create(AccountObj);
            }
            else
            {
                new AccountToEventHandler().Delete(AccountObj.);
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


        private void Availability()   //Denne er nok pænt kluntet men det er trods alt et forsøg.
        {
            List<Room> RoomList = new RoomHandler().Read();

            foreach (var r in RoomList.Where(r => r.Event.Equals(SikonSingleton.SelectedEvent)))
            {
                if (r.Capacity < (r.Capacity * 0.8))
                {
                    Console.WriteLine("Ledig pladser");
                }
                else if (r.Capacity == r.Capacity)
                {
                    Console.WriteLine("Fuld Booket");
                }
                else
                {
                    Console.WriteLine("Få ledige pladser");
                }
            }
        }

    }
}
