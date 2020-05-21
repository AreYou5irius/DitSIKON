using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Calls;
using SIKONClassLibrary;
using SIKONClassLibrary.EventHandlers;
using SIKONClient.Common;
using SIKONClient.Model;

namespace SIKONClient
{
    class ViewModelKursusSide
    {

        private ICommand _tilmeldCommand;
        private ICommand _sendCommand;

        public Singleton SikonSingleton { get; set; }

        public ObservableCollection<Question> QuestionList { get; set; } // vi opretter en reference til vores OC af question

        public Question UserQuestion { get; set; }

        public ICommand TilmeldCommand { get; set; }

        public ICommand SendCommand { get; set; }

        public ViewModelKursusSide()
        {

            TilmeldCommand = new RelayCommand(AddEventToAccount);

            SendCommand = new RelayCommand(AddQuestionToEvent);

            SikonSingleton = Singleton.Instance;



        }

        private void AddEventToAccount()
        {
            List<Event> Eventliste = new EventsHandler().Read();
            foreach (var e in Eventliste.Where(e => e.ID == SikonSingleton.SelectedEvent.ID))
            {
                if (SikonSingleton != null)
                {
                    SikonSingleton.LoggedAccount.Event.Add(e);
                }

            }

        }

        private void AddQuestionToEvent()
        {
            List<Question> liste = new QuestionHandler().Read();

            QuestionList = new ObservableCollection<Question>();
            foreach (var q in liste.Where(q => q.Event_ID == SikonSingleton.SelectedEvent.ID))
            {
                QuestionList.Add(q);
            }

            UserQuestion = new Question();

        }
    }
}
