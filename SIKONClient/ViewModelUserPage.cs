using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIKONClassLibrary;
using SIKONClient.Model;


namespace SIKONClient
{
    class ViewModelUserPage
    {
        public Singleton SikonSingleton { get; set; }

        public AccountToEvent AccountToEvent { get; set; }

        public ObservableCollection<AccountToEvent> MyEventsList { get; set; }

        public ViewModelUserPage()
        {

            SikonSingleton = Singleton.Instance;

            List<AccountToEvent> liste = new List<AccountToEvent>();

            MyEventsList = new ObservableCollection<AccountToEvent>();
            foreach (var a in liste.Where(a => a.Account_ID == SikonSingleton.LoggedAccount.Email))
            {
                MyEventsList.Add(a);
            }

            MyEventsList = new ObservableCollection<AccountToEvent>();

        }



    }
}
