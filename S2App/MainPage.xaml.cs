using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DataAccessLibrary;
using Windows.UI.Popups;
using Windows.ApplicationModel.Core;
using System;
using System.Collections.Generic;


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


            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            DataAccess.InitializeDatabase();
            List<string> users = DataAccess.GetUserList();
            
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
