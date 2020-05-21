using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace SIKONClassLibrary.EventHandlers
{
    class MessageDialogHelper
    {
        public static async void Show(string content, string title)
        {
                MessageDialog messageDialog = new MessageDialog(content, title);
                await messageDialog.ShowAsync();
        }
    }
}
