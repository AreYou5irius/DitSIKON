using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

            List<Room> roomList = new RoomHandler().Read();


            foreach (var ev in liste)
            {
                foreach (var ro in roomList)
                {
                    if (ev.Room_ID == ro.ID)
                    {
                        ev.Room = ro;

                    }
                }
            }



            try
            {
                KursusListe = new ObservableCollection<Event>(liste);

            }
            catch (Exception e)
            {
                
            }
            

            //foreach (var VARIABLE in liste)
            //{
            //    KursusListe.Add(VARIABLE);
            //}
            //KursusListe = new ObservableCollection<Event>(new EventsHandler().Read()); // Her opretter vi en liste/OC af events som henter data via read fra vores eventhandler db
        }



    }

}
