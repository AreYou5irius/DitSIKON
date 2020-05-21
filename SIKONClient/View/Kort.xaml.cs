using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SIKONClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Kort : Page
    {
        public Kort()
        {
            this.InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Etage = e.AddedItems[0].ToString();
            
            switch (Etage)
            {
                
                case "Stuen":
                    Stuen.Visibility = Visibility.Visible;
                    Foerste.Visibility = Visibility.Collapsed;
                    break;
                case "Foerste":
                    Stuen.Visibility = Visibility.Collapsed;
                    Foerste.Visibility = Visibility.Visible;
                    break;
            }
            
        }
    }
}
