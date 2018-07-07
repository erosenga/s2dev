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
    public sealed partial class CreateUserPage : Page
    {
        public CreateUserPage()
        {
            this.InitializeComponent();
            List<String> users = DataAccess.GetUserList();
            users.Add("New User");
            SelectUserBox.Items.Clear();
            SelectUserBox.ItemsSource = users;
           

        }

        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(WelcomeUserPage));
        }

        private void ButtonProtocols_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonRunLog_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(RunLogPage));
        }

        private void ButtonAdministration_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(AdministrationPage));
        }

        private void ButtonSignOut_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentUser = null;
            ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
        }

        private void SelectUserBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User localuser=null;
            if (SelectUserBox.SelectedItem == null)
                return;
            if ((string)SelectUserBox.SelectedItem == "New User")
            {
                
                FirstName.Text = "";
                Lastname.Text = "";
                email.Text = "";
                Telephone.Text = "";
                Password.Text = "";
                RunOnly.IsChecked = true; 
                Comment.Text = "";
            }
             else
            {
                localuser = DataAccess.GetUser(SelectUserBox.SelectedItem.ToString());
                FirstName.Text = localuser.FirstName;
                Lastname.Text = localuser.LastName;
                email.Text = localuser.email;
                Telephone.Text = localuser.Telephone;
                Password.Text = localuser.Password;
                if (localuser.Privilege == 0)
                    { Admin.IsChecked = true; }
                else if (localuser.Privilege == 1)
                    { Full.IsChecked = true; }
                else if (localuser.Privilege == 2)
                    { RunOnly.IsChecked = true; }
                Comment.Text = localuser.Comment;
               

            }

         }

        

        private async void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            int id = (int)DataAccess.GetUserId(email.Text);
            if (id < 2)
            {
                var dialog1 = new MessageDialog("Invalid user, cannot delete");
                var result1 = await dialog1.ShowAsync();
                return;
            }
            MessageDialog showDialog = new MessageDialog("Are you sure you want to deactivate this User?");
            showDialog.Commands.Add(new UICommand("Yes")
            {
                Id = 0
            });
            showDialog.Commands.Add(new UICommand("No")
            {
                Id = 1
            });
            showDialog.DefaultCommandIndex = 0;
            showDialog.CancelCommandIndex = 1;
            var result = await showDialog.ShowAsync();
            if ((int)result.Id == 0)
            {
                FirstName.Text = "";
                Lastname.Text = "";
                email.Text = "";
                Telephone.Text = "";
                Password.Text = "";
                RunOnly.IsChecked = true;
                Comment.Text = "";
                DataAccess.DeleteUserRecord(id);
                var dialog = new MessageDialog("User deleted!");
                var result1 = await dialog.ShowAsync();
                List<String> users = DataAccess.GetUserList();
                users.Add("New User");
                SelectUserBox.ItemsSource = users;
                return;
            }
            else
            {
                return; 
            }
        }

        private async void SaveUser_Click(object sender, RoutedEventArgs e)
        {

            if (email.Text == "")
            {
                var dialog2 = new MessageDialog("email is required as Id");
                var result2 = await dialog2.ShowAsync();
                return;
            }
            if (SelectUserBox.SelectedItem.ToString() == "r")
            {
                var dialog2 = new MessageDialog("Please Select User");
                var result2 = await dialog2.ShowAsync();
                return;
            }
            if (SelectUserBox.SelectedItem.ToString() == "New User")
            {
                int id1 = (int)DataAccess.GetUserId(email.Text);
                if (id1 > 0)
                {
                    var dialog4 = new MessageDialog("email (id) already exists, cannot save");
                    var result2 = await dialog4.ShowAsync();
                    return;
                }
            }
            

            if (!((bool)Admin.IsChecked || (bool)Full.IsChecked || (bool)RunOnly.IsChecked))
            {
                var dialog3 = new MessageDialog("Privilege Level is required");
                var result3 = await dialog3.ShowAsync();
                return;
            }
            User localuser = new User();
            localuser.FirstName=FirstName.Text ;
            localuser.LastName=Lastname.Text ;
            localuser.email=email.Text ;
            localuser.Telephone=Telephone.Text;
            localuser.Password=Password.Text;
            if ((bool)Admin.IsChecked)
                localuser.Privilege = 0;
            else if ((bool)Full.IsChecked)
                localuser.Privilege = 1;
            else if ((bool)RunOnly.IsChecked)
                localuser.Privilege = 2;
            Comment.Text = Comment.Text.Replace("\'", "\'\'");
            Comment.Text = Comment.Text.Replace("\"", "\"\"");
            localuser.Comment=Comment.Text;
            
            int id = (int)DataAccess.GetUserId(localuser.email);
            if (id == -1)
                DataAccess.InsertNewUserRecord(localuser.FirstName, localuser.LastName, localuser.email, localuser.Telephone, localuser.Password, localuser.Privilege.ToString(), localuser.Comment);
            else
                DataAccess.UpdateUserRecord(id, localuser.FirstName, localuser.LastName, localuser.email, localuser.Telephone, localuser.Password, localuser.Privilege.ToString(), localuser.Comment);
            var dialog = new MessageDialog("User saved!");
            var result1 = await dialog.ShowAsync();
            ((Frame)Window.Current.Content).Navigate(typeof(CreateUserPage));
            return;

        }
    }
}
