using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Store.Preview.InstallControl;
using SIKONClassLibrary;
using SIKONClassLibrary.EventHandlers;
using SIKONClient.Annotations;
using SIKONClient.Common;
using SIKONClient.Model;


namespace SIKONClient.ViewModel
{
    class ViewModelKurser : INotifyPropertyChanged
    {
        private string _sortBy;
        private ObservableCollection<Event> _kursusListe;

        public Singleton SikonSingleton { get; set; }
        public List<Event> EventListe { get; set; }
        public List<string> ComboBoxStrings { get; set; }

        public ICommand SortListCommand { get; set; }

        public ObservableCollection<Event> KursusListe
        {
            get => _kursusListe;
            set { _kursusListe = value; OnPropertyChanged("KursusListe"); }
        } // vi opretter en reference til vores OC af events

        public string SortBy
        {
            get => _sortBy;
            set { _sortBy = value; SortEvents(); }
        }

        public ViewModelKurser()
        {
            SikonSingleton = Singleton.Instance;
            SikonSingleton.SelectedEvent = null;

            SortListCommand = new RelayCommand(SortEvents);
            ComboBoxStrings = new List<string>()
            {
                "Emne",
                "Kategori",
                "Lokale",
                "Dato"
            };

            EventListe = new EventsHandler().Read();
            List<Room> roomList = new RoomHandler().Read();
            List<TimeToEvent> timeList = new TimeToEventHandler().Read();

            if (EventListe != null) { 
                foreach (var ev in EventListe)
                {
                    foreach (var ro in roomList)
                    {
                        if (ev.Room_ID == ro.ID)
                        {
                            ev.Room = ro;
                        }
                    }
                }

                foreach (var ev in EventListe)
                {
                    foreach (var ti in timeList)
                    {
                        if (ev.ID == ti.Event_ID)
                        {
                            ev.TimeToEvent.Add(ti);
                            ev.Time = ti;
                        }
                    }
                }


                var sortedList = from e in EventListe
                    orderby e.Subject
                    select e;

                KursusListe = new ObservableCollection<Event>(sortedList);

            }
        }

        /// <summary>
        /// Sortere listen af Events udfra en switch case og linq
        /// </summary>
        public void SortEvents()
        {
            switch (SortBy)
            {
                    
                case "Emne":
                    var sortSub = from e in EventListe
                        orderby e.Subject
                        select e;
                    KursusListe = new ObservableCollection<Event>(sortSub);
                    break;
                case "Kategori":
                    var sortCat = from e in EventListe
                        orderby e.Category
                        select e;
                    KursusListe = new ObservableCollection<Event>(sortCat);
                    break;
                case "Lokale":
                    var sortRoo = from e in EventListe
                        orderby e.Room_ID 
                        select e;
                    KursusListe = new ObservableCollection<Event>(sortRoo);
                    break;
                case "Dato":
                    var sortDat = from e in EventListe
                        orderby e.Time
                        select e;
                    KursusListe = new ObservableCollection<Event>(sortDat);
                    break;
                default:
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
