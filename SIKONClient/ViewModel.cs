using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIKONClassLibrary;
using SIKONClient.EventHandlers;


namespace SIKONClient
{
    class ViewModel
    {
        public ObservableCollection<Event> KursusListe; // vi opretter en reference til vores OC af events


        
        public ViewModel()
        {
            KursusListe = new ObservableCollection<Event>(new EventsHandler().Read()); // Her opretter vi en liste/OC af events som henter data via read fra vores db
        }



    }

}
