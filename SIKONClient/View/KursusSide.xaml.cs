﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SIKONClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class KursusSide : Page
    {
        private Singleton sikonSingleton;

        

        public KursusSide()
        {
            sikonSingleton = Singleton.Instance;
            this.InitializeComponent();

            if (sikonSingleton.LoggedAccount != null)
            {
                if (sikonSingleton.LoggedAccount.AccountType == "A" || sikonSingleton.LoggedAccount.AccountType == "S")
                {
                    lists.Visibility = Visibility.Visible;
                    
                }

                if (sikonSingleton.LoggedAccount.AccountType == "A")
                {
                    SletKursus.Visibility = Visibility.Visible;
                }

                if (sikonSingleton.LoggedAccount.AccountType == "A")
                {
                    ChangeRoom.Visibility = Visibility.Visible;
                }

            }

        }

        private void RerouteToKurser_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Kurser));
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(KursusSide));
        }
    }
}
