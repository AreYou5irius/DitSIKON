using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SIKONClassLibrary;
using SIKONClassLibrary.EventHandlers;
using SIKONClient.Common;
using SIKONClient.Model;

namespace SIKONClient.ViewModel
{
    class ViewModelOpretBruger
    {
        public ICommand OpretBruger { get; set; }

        private Account AccountObj;
        public Singleton SikonSingleton { get; set; }

        public string ID { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public ViewModelOpretBruger()
        {
            SikonSingleton = Singleton.Instance;

            OpretBruger = new RelayCommand(Opret);

            AccountObj = new Account();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Opret()
        {
            AccountObj.Name = Name;
            AccountObj.Email = ID;
            AccountObj.Password = Password;
            AccountObj.AccountType = "U";


            new AccountHandler().Create(AccountObj);
            SikonSingleton.LoggedAccount = new AccountHandler().ReadFrom(ID);
        }
    }
}
