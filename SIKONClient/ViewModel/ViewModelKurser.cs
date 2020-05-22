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


namespace SIKONClient.ViewModel
{
    class ViewModelKurser
    {
       
        public Singleton SikonSingleton { get; set; }

       
        public ObservableCollection<Event> KursusListe { get; set; } // vi opretter en reference til vores OC af events

        public ViewModelKurser()
        {
            SikonSingleton = Singleton.Instance;

            SikonSingleton.SelectedEvent = null;

            List<Event> liste = new EventsHandler().Read();

            //List < Room > Roomlist = new RoomHandler().Read();
            //foreach (var ev in liste)
            //{
            //    foreach (var no in Roomlist)
            //    {
            //        if (ev.Room.ID == no.Capacity)
            //        {
            //            ev.Room = no;
            //        }
            //    }
            //}

            try
            {
                KursusListe = new ObservableCollection<Event>(liste);
            }
            catch (Exception e)
            {
                
            }

           
        }

        
    }

}
