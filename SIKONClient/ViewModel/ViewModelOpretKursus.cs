using System;
using System.Collections.Generic;
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

        

        public ViewModelOpretKursus()
        {
            

            OpretKursus = new RelayCommand(OpretK);
            EventObj = new Event();


           

        }
        public void OpretK()
        {
            EventObj.Subject = Subject;
            EventObj.Speaker = Speaker;
            EventObj.Category = Category;
            EventObj.Description = Description;

            new EventsHandler().Create(EventObj);
        }

    }
}
