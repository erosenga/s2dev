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
using DataAccessLibrary;
using Windows.UI.Popups;

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
            List<string> localspecies= new List<string>();
            localspecies.Add("Any");
            localspecies.AddRange(DataAccess.GetSpeciesList());
            Species.ItemsSource = localspecies;
            Species.SelectedIndex = 0;
            List<string> localtissue = new List<string>();
            localtissue.Add("Any");
            localtissue.AddRange(DataAccess.GetOrganList());
            Tissue.ItemsSource = localtissue;
            Tissue.SelectedIndex = 0;
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            var temp = Type.Items[Type.SelectedIndex] as ComboBoxItem;
            string temp1 = temp.Content.ToString();
            
            var mylist=DataAccess.GetRecipeList(ProtocolName.Text, temp1, Species.SelectedItem.ToString(), Tissue.SelectedItem.ToString());
            ((Frame)Window.Current.Content).Navigate(typeof(RecipeGrid1),mylist);
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

        private async void ButtonNewProtocol_Click(object sender, RoutedEventArgs e)
        {
            if (App.CurrentUser.Privilege > 1)
            {
                var dialog = new MessageDialog("You are not allowed to Create a new recipe (insufficient Privilege)");
                var result1 = await dialog.ShowAsync();
                return;
            }
            ((Frame)Window.Current.Content).Navigate(typeof(CreateProtocolPage));
        }

        private void ButtonViewAllProtocols_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(RecipeGrid1));
        }
    }
}
