using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Contacts.DataProvider;
using SIKONClassLibrary;
using SIKONClassLibrary.EventHandlers;
using SIKONClient.Common;
using SIKONClient.Model;


namespace SIKONClient.ViewModel
{
    public class ViewModelUserPage
    {
        public ICommand UpdateUserInfoCommand { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Singleton SikonSingleton { get; set; }

        public AccountToEvent AccountToEvent { get; set; }

        public ObservableCollection<Event> MyEventsList { get; set; }

        public ViewModelUserPage()
        {
            SikonSingleton = Singleton.Instance;

            UpdateUserInfoCommand = new RelayCommand(UpdateUserInfo);

            Name = SikonSingleton.LoggedAccount.Name;
            Email = SikonSingleton.LoggedAccount.Email;
            Password = SikonSingleton.LoggedAccount.Password;


            List<AccountToEvent> AccountToEventListe = new AccountToEventHandler().Read();

            List<Event> EventListe = new EventsHandler().Read();

            MyEventsList = new ObservableCollection<Event>();

            List<AccountToEvent> TilmeldteEvents = new List<AccountToEvent>();

            foreach (var g in AccountToEventListe.Where(g => SikonSingleton.LoggedAccount.Email == g.Account_ID))
            {
                TilmeldteEvents.Add(g);
                    
            }

            foreach (var w in TilmeldteEvents)
            {
                foreach (var r in EventListe)
                {
                    if (w.Event_ID == r.ID)
                    {
                        MyEventsList.Add(r);
                    }
                }
            }

        }

        public void UpdateUserInfo()
        {
            SikonSingleton.LoggedAccount.Name = Name;
           
            SikonSingleton.LoggedAccount.Password = Password;
            new AccountHandler().Update(SikonSingleton.LoggedAccount, SikonSingleton.LoggedAccount.Email);
        }
    }

}



