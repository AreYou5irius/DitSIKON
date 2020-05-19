using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIKONClassLibrary;
using SIKONClient.Model;

namespace SIKONClient
{
    class ViewModelMainPage
    {
        public Singleton SikonSingleton { get; set; }    

        public ViewModelMainPage()
        {
            SikonSingleton = Singleton.Instance;
        }


    }
}
