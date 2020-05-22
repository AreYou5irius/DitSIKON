using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIKONClassLibrary;
using SIKONClient.Model;

namespace SIKONClient.ViewModel
{
    class ViewModelMainPage
    {
        public Singleton SikonSingleton { get; set; }

        public string AccountButton { get; set; }
        public string LogButton { get; set; }


        public ViewModelMainPage()
        {
            SikonSingleton = Singleton.Instance;

            if (SikonSingleton.LoggedAccount == null)
            {
                AccountButton = "Opret Bruger";
                LogButton = "Log Ind";
            }
            else
            {
                AccountButton = SikonSingleton.LoggedAccount.Name;
                LogButton = "Log Ud";
            }


        }
        

    }
}
