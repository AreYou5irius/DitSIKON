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
using SIKONClient.Model;

namespace SIKONClient
{
    class ViewModelKursusSide
    {
       // private ICommand  _tilmeldCommand;

        public Singleton SikonSingleton { get; set; }

        public ObservableCollection<Question> QuestionList { get; set; } // vi opretter en reference til vores OC af question
        
        public Question UserQuestion { get; set; }

        //public ICommand TilmeldCommand { get; set; }  //Hertil er jeg nået 19. maj MAJA lav Icommand færdig

        public ViewModelKursusSide()
        {
        
           SikonSingleton = Singleton.Instance;

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
