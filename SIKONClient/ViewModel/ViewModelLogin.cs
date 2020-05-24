using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using SIKONClassLibrary;
using SIKONClassLibrary.EventHandlers;
using SIKONClient.Annotations;
using SIKONClient.Common;
using SIKONClient.Model;

namespace SIKONClient.ViewModel
{
    class ViewModelLogin : INotifyPropertyChanged
    {
        private string _id;
        private string _password;
        
        public ICommand Login { get; set; }

        public string Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged("Id");}
        }

        public string Password
        {
            get => _password;
            set {_password = value; OnPropertyChanged("Password");}
        }

        public Singleton SikonSingleton { get; set; }

        public ViewModelLogin()
        {
            Login = new RelayCommand(LoginAccount);
            
            SikonSingleton = Singleton.Instance;
        }

        private void LoginAccount()
        {
            try
            {
                SikonSingleton.LoggedAccount = new AccountHandler().LogIn(Id, Password);
            }
            catch (Exception e)
            {
                MessageDialogHelper.Show(e.Message,"Login Fejl");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
