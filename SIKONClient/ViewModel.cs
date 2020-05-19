using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store.Preview.InstallControl;
using SIKONClassLibrary;
using SIKONClassLibrary.EventHandlers;
using SIKONClient.Model;


namespace SIKONClient
{
    class ViewModel
    {

        public Singleton SikonSingleton { get; set; }

     
        public ObservableCollection<Event> KursusListe { get; set; } // vi opretter en reference til vores OC af events

        public ViewModel()
        {
            SikonSingleton = Singleton.Instance;
            
            List<Event> liste = new EventsHandler().Read();

            KursusListe = new ObservableCollection<Event>();
            foreach (var VARIABLE in liste)
            {
                KursusListe.Add(VARIABLE);
            }
            //KursusListe = new ObservableCollection<Event>(new EventsHandler().Read()); // Her opretter vi en liste/OC af events som henter data via read fra vores eventhandler db
        }



    }

}
