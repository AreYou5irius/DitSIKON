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

namespace SIKONClient
{
    class ViewModelOpretBruger
    {
        private Account AccountObj;

        public ICommand OpretBruger { get; set; }

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

        public void Opret()
        {
            AccountObj.Name = Name;
            AccountObj.Email = ID;
            AccountObj.Password = Password;


            new AccountHandler().Create(AccountObj);
            SikonSingleton.LoggedAccount = new AccountHandler().ReadFrom(ID);
            
        }


    }
}
