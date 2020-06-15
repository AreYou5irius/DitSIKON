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
using SIKONClient.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SIKONClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Singleton SikonSingleton = Singleton.Instance;

        public MainPage()
        {
            this.InitializeComponent();
            Frame1.Navigate((typeof(Home)));

            if (SikonSingleton == null) Frame.Navigate(typeof(MainPage));

        }

        private void Kurser_Click(object sender, RoutedEventArgs e)
        {
            Frame1.Navigate(typeof(Kurser));
        }

        private void Kort_OnClick(object sender, RoutedEventArgs e)
        {
            Frame1.Navigate(typeof(Kort));
        }

        private void Info_OnClick(object sender, RoutedEventArgs e)
        {
            Frame1.Navigate(typeof(Info));
        }

        private void Home_OnClick(object sender, RoutedEventArgs e)
        {
            Frame1.Navigate(typeof(Home));
        }

        private void UserPage_OnClick(object sender, RoutedEventArgs e)
        {
            if (SikonSingleton.LoggedAccount != null) { 
                Frame1.Navigate(typeof(UserPage));
            }
            else
            {
                Frame1.Navigate(typeof(OpretBruger));
            }

        }

        private async void Login_OnClick(object sender, RoutedEventArgs e)
        {
            if (SikonSingleton.LoggedAccount == null)
            {
                Frame1.Navigate(typeof(Login));
            }
            else
            {
                SikonSingleton.LoggedAccount = null;

                Frame.Navigate(typeof(MainPage));
                ContentDialog dialog = new ContentDialog(){Content = "Du er nu logget ud!", CloseButtonText = "Ok"};
                dialog.ShowAsync();
            }
        }

        private void Program_Click(object sender, RoutedEventArgs e)
        {
            Frame1.Navigate(typeof(ProgramView));
        }
    }
}
