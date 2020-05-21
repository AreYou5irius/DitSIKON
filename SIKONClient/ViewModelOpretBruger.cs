using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SIKONClient.Common;

namespace SIKONClient
{
    class ViewModelOpretBruger
    {
        public ICommand OpretBruger { get; set; }

        public string ID { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public ViewModelOpretBruger()
        {
            OpretBruger = new RelayCommand(Opret);
        }

        public void Opret()
        {
            throw new NotImplementedException();
        }


    }
}
