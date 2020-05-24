using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SIKONClassLibrary;
using SIKONClassLibrary.EventHandlers;
using SIKONClient.Annotations;
using SIKONClient.Common;

namespace SIKONClient.ViewModel
{
    class ViewModelOpretKursus : INotifyPropertyChanged
    {
        private Event EventObj;
        private TimeSpan _selectedTime;
        private DateTime _combinedDate;
        private DateTimeOffset _selectedDate;
        public int ID { get; set; }
        public string Subject { get; set; }
        public string Category { get; set; }
        public string Speaker { get; set; }
        public string Description { get; set; }
        public ICommand OpretKursus { get; set; }
        public Room SelectedRoom { get; set; }
        public ObservableCollection<Room> RoomList { get; set; }
        public List<string> Days { get; set; }
        public string SelectedDay { get; set; }

        public DateTimeOffset SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged("SelectedTime");
                CombinedDate = SelectedDate.DateTime.Date + SelectedTime;
            }
        }

        public TimeSpan SelectedTime
        {
            get => _selectedTime;
            set {
                _selectedTime = value;
                OnPropertyChanged("SelectedTime");
                CombinedDate = SelectedDate.DateTime.Date + SelectedTime;
            }
        }

        public DateTime CombinedDate
        {
            get => _combinedDate;
            set
            {
                _combinedDate = value;
                OnPropertyChanged("CombinedDate");
            }
        }

        public ViewModelOpretKursus()
        {
            OpretKursus = new RelayCommand(OpretK);
            EventObj = new Event();
            RoomList = new ObservableCollection<Room>(new RoomHandler().Read());

            SelectedTime = DateTime.Now.TimeOfDay;
            SelectedDate = DateTime.Now.Date;
        }

        public void OpretK()
        {
            EventObj.Subject = Subject;
            EventObj.Speaker = Speaker;
            EventObj.Category = Category;
            EventObj.Description = Description;
            if (SelectedRoom != null) EventObj.Room_ID = SelectedRoom.ID;
            TimeToEvent timeToEvent = new TimeToEvent();
            timeToEvent.Time = CombinedDate;

            EventObj.TimeToEvent.Add(timeToEvent);

            new EventsHandler().Create(EventObj);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
