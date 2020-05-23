using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SIKONClassLibrary;
using SIKONClassLibrary.EventHandlers;
using SIKONClient.Common;

namespace SIKONClient.ViewModel
{
    class ViewModelOpretKursus
    {
        private Event EventObj;
        public int ID { get; set; }
        public string Subject { get; set; }
        public string Category { get; set; }
        public string Speaker { get; set; }
        public string Description { get; set; }
        public ICommand OpretKursus { get; set; }
        public Room SelectedRoom { get; set; }
        public ObservableCollection<Room> RoomList { get; set; }

        public ViewModelOpretKursus()
        {
            OpretKursus = new RelayCommand(OpretK);
            EventObj = new Event();
            RoomList = new ObservableCollection<Room>(new RoomHandler().Read());
        }
        public void OpretK()
        {
            EventObj.Subject = Subject;
            EventObj.Speaker = Speaker;
            EventObj.Category = Category;
            EventObj.Description = Description;
            EventObj.Room_ID = SelectedRoom.ID;

            new EventsHandler().Create(EventObj);
        }



    }
}
