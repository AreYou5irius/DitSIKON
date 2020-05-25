using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using SIKONClassLibrary;
using SIKONClassLibrary.EventHandlers;
using SIKONClient.Annotations;
using SIKONClient.Common;
using SIKONClient.Model;

namespace SIKONClient.ViewModel
{
    class ViewModelMainPage : INotifyPropertyChanged
    {
        private string _accountButton;
        private string _logButton;
        public Singleton SikonSingleton { get; set; }

        public string AccountButton
        {
            get => _accountButton;
            set { _accountButton = value; OnPropertyChanged("AccountButton"); }
        }

        public string LogButton
        {
            get => _logButton;
            set { _logButton = value; OnPropertyChanged("LogButton"); }
        }

        public ICommand Refresh { get; set; }

        public ViewModelMainPage()
        {
            SikonSingleton = Singleton.Instance;

            //SikonSingleton.LoggedAccount = new AccountHandler().ReadFrom("admin@sikon.dk");

            Refresh = new RelayCommand(RefreshButtonText);

            RefreshButtonText();

            //Thread thread = new Thread(() => RefreshButtonText());
            //thread.Start();
            //SikonSingleton.LoggedAccount= new AccountHandler().ReadFrom("Admin@sikon.dk");
        }

        /// <summary>
        /// Opdaterer teksten på knapperne
        /// </summary>
        private void RefreshButtonText()
        {
            if (SikonSingleton.LoggedAccount != null)
            {
                AccountButton = SikonSingleton.LoggedAccount.Name;
                LogButton = "Log Ud";
            }
            else
            {
                AccountButton = "Opret Bruger";
                LogButton = "Log Ind";
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
