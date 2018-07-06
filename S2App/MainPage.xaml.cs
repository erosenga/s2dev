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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace S2App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static int selectedId;
        public MainPage()
        {
            this.InitializeComponent();
            DataAccess.InitializeDatabase();
            List<String> users = DataAccess.GetUserList();
            
            UserList.Items.Clear();
            selectedId = -1;
            UserList.ItemsSource = users;
        }

        private async void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (UserList.SelectedItem == null)
                return;
            string dbpasswd = DataAccess.GetPassword(UserList.SelectedItem.ToString());
            if (Password.Password == dbpasswd)
            {
                App.CurrentUser = DataAccess.GetUser(UserList.SelectedItem.ToString());
                ((Frame)Window.Current.Content).Navigate(typeof(WelcomeUserPage));
            }
            else
            {
                var dialog = new MessageDialog("Invalid User ID/Password combination");
                var result1 = await dialog.ShowAsync();

            }
            return;
        }
        
       
        private void User_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
