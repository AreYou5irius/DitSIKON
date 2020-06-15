using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Contacts.DataProvider;
using Windows.UI.Xaml.Controls;
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
        
        public ObservableCollection<Event> MyEventsList { get; set; }

        public ViewModelUserPage()
        {
            SikonSingleton = Singleton.Instance;
            SikonSingleton.SelectedEvent = null;

            UpdateUserInfoCommand = new RelayCommand(UpdateUserInfo);

            Name = SikonSingleton.LoggedAccount.Name;
            Email = SikonSingleton.LoggedAccount.Email;
            Password = SikonSingleton.LoggedAccount.Password;

            MyEventsList = MyEvents();

        }

        private ObservableCollection<Event> MyEvents()
        {

            List<AccountToEvent> AccountToEventListe = new AccountToEventHandler().Read();
            List<Event> EventListe = new EventsHandler().Read();
            ObservableCollection<Event> UserEvents = new ObservableCollection<Event>();
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
                        UserEvents.Add(r);
                    }
                }
            }

            return UserEvents;
        }
        /// <summary>
        /// Opdaterer bruger info
        /// </summary>
        public async void UpdateUserInfo()
        {
            SikonSingleton.LoggedAccount.Name = Name;
           
            SikonSingleton.LoggedAccount.Password = Password;
            new AccountHandler().Update(SikonSingleton.LoggedAccount, SikonSingleton.LoggedAccount.Email);

            ContentDialog dialog = new ContentDialog(){Content = "Dine bruger informationer er nu updateret", CloseButtonText = "Ok"};
            dialog.ShowAsync();

        }
    }
}