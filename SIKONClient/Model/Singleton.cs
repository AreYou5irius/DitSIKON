using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIKONClassLibrary;

namespace SIKONClient.Model
{
    public class Singleton
    {
        private static Singleton _instance = null;

        public static Singleton Instance
        {
            get { return _instance ?? (_instance = new Singleton()); }
        }

        public Account LoggedAccount { get; set; }

        public Event SelectedEvent { get; set; }

        public Singleton()
        {
            LoggedAccount = null;
            SelectedEvent = null;
        }

    }
}
