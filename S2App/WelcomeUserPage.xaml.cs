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

namespace S2App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WelcomeUserPage : Page
    {
        public WelcomeUserPage()
        {
            this.InitializeComponent();
            WelcomeBanner.Text = "Welcome, " + App.CurrentUser.email;
            if(App.CurrentUser.Privilege>0)
            {
                ButtonAdministration.IsEnabled = false;
                ButtonAdministration.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                ButtonAdministration.IsEnabled = true;
                ButtonAdministration.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {

        }

       
        private void ButtonAdministration_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(AdministrationPage));
        }

        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {


        }

        private void ButtonRunLog_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(RunLogPage));
        }

        private void ButtonSignOut_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentUser = null;
            ((Frame)Window.Current.Content).Navigate(typeof(MainPage));

        }

        private void ButtonNewProtocol_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(CreateProtocolPage));
        }

        private void ButtonViewAllProtocols_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
