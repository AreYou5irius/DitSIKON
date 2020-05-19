using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Eventmaker.Common;

namespace SIKONClient
{
    class ViewModelLogin
    {
        public ICommand Login { get; set; }
        
        public string Id { get; set; }
        public string Password { get; set; }

        public ViewModelLogin()
        {
            Login = new RelayCommand(LoginAccount);
        }

        private void LoginAccount()
        {
            throw new NotImplementedException();
        }
    }
}
