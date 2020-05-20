using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIKONClassLibrary;
using SIKONClassLibrary.EventHandlers;
using SIKONClient.Model;


namespace SIKONClient
{
    public class ViewModelUserPage
    {
        public Singleton SikonSingleton { get; set; }

        public AccountToEvent AccountToEvent { get; set; }

        public ObservableCollection<AccountToEvent> MyEventsList { get; set; }

        public ViewModelUserPage()
        {

            SikonSingleton = Singleton.Instance;

            List<AccountToEvent> liste = new AcccountToEventHandler().Read();

            MyEventsList = new ObservableCollection<AccountToEvent>();
            foreach (var g in liste.Where(g => SikonSingleton.LoggedAccount.Email == g.Account_ID))
            {

                MyEventsList.Add(g);

            }


        }

    }

}



