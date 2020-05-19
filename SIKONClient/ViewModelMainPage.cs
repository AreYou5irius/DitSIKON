using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIKONClassLibrary;

namespace SIKONClient
{
    class ViewModelMainPage
    {
        public Account LoggedIn { get; set; }
        public Event SelectedEvent { get; set; }

        public ViewModelMainPage()
        {
            LoggedIn = null;
            SelectedEvent = null;

        }


    }
}
