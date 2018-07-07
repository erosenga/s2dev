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
using Microsoft.Data.Sqlite;
using DataAccessLibrary;
using System.Data;
using Windows.UI.Popups;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace S2App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 


    public sealed partial class RecipeGrid1 : Page
    {
        public List<Recipe> ds;

        public RecipeGrid1()
        {
            this.InitializeComponent();
           

        }
         protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<Recipe> inlist= e.Parameter as List<Recipe>;
            if (inlist != null)
            {
                ds = inlist;
            }
            else ds = DataAccess.GetRecipeList();
            rgrid.ItemsSource = ds;
            rgrid.AutoGenerateColumns = true;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = rgrid.SelectedItem as Recipe;
            selectedItem.Tag = "Run";
            ((Frame)Window.Current.Content).Navigate(typeof(ViewProtocolPage), selectedItem);
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = rgrid.SelectedItem as Recipe;
            if (selectedItem != null)
            {
                if (selectedItem.Lock != 0)
                    if (App.CurrentUser.Id != selectedItem.Owner)
                    {
                        var dialog = new MessageDialog("You are not the owner of this locked recipe, please copy recipe to edit");
                        var result1 = await dialog.ShowAsync();
                        return;
                    }

                selectedItem.Tag = "Edit";
                ((Frame)Window.Current.Content).Navigate(typeof(ViewProtocolPage), selectedItem);
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = rgrid.SelectedItem as Recipe;
            if (selectedItem != null)
            {
                if (selectedItem.Lock != 0)
                    if (App.CurrentUser.Id != selectedItem.Owner)
                    {
                        var dialog = new MessageDialog("You are not the owner of this locked recipe, Delete is not allowed");
                        var result1 = await dialog.ShowAsync();
                        return;
                    }

                selectedItem.Tag = "Delete";
                ((Frame)Window.Current.Content).Navigate(typeof(ViewProtocolPage), selectedItem);
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentUser.Id = -1;
            ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {

            ((Frame)Window.Current.Content).Navigate(typeof(ViewProtocolPage));
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = rgrid.SelectedItem as Recipe;
            if (selectedItem != null)
            {
                Recipe newrecipe = new Recipe();
                newrecipe = selectedItem;
                newrecipe.NickName = "Copyof" + selectedItem.NickName;
                newrecipe.Id = -1;
                newrecipe.Tag = "Copy";
                newrecipe.Owner = App.CurrentUser.Id;
                newrecipe.Lock = 0;

                ((Frame)Window.Current.Content).Navigate(typeof(ViewProtocolPage), newrecipe);
            }
        }

        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(WelcomeUserPage));
        }

        private void ButtonProtocols_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(ViewProtocolPage));
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
            ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
        }
    }
}
