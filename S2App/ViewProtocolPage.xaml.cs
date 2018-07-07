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
    public sealed partial class ViewProtocolPage : Page
    {
        public ViewProtocolPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Recipe recipe = e.Parameter as Recipe;
            
            if (recipe != null)
            {
                App.CurrentRecipe = recipe;
                Protocol.Text = recipe.NickName;
                Species.Text = recipe.Species;
                Tissue.Text = recipe.Organ;
                DisruptionTime.Text = recipe.GrindTime.ToString();
                DisruptionTemp.Text = recipe.GrindTemp.ToString();
                DisruptionSpeed.Text = recipe.RPM.ToString();
                IncubationTime.Text = recipe.IncubationTime.ToString();
                IncubationTemperature.Text = recipe.IncubationTemp.ToString();
                Cycles.Text = recipe.Cycles.ToString();
                Enzyme.Text = recipe.Enzyme.ToString();
                if (recipe.Type == 0) Type.Text = "Single Cell";
                if (recipe.Type == 1) Type.Text = "Nuclei";
                if (recipe.Type == 2) Type.Text = "Nucleic Acid";
                if (recipe.State == 0) State.Text = "Normal";
                if (recipe.State == 1) State.Text = "Cancerous";
                Lock.IsChecked = (recipe.Lock != 0);
                Notes.Text = recipe.Comment;
                

            }
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

        private void PrepareSampleButton_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(InsertSamplePage), App.CurrentRecipe);

        }

        private void DuplicateButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentRecipe.Tag = "Create";
            App.CurrentRecipe.Id = -1;
            App.CurrentRecipe.NickName = "CopyOf_" + App.CurrentRecipe.NickName;
            App.CurrentRecipe.Owner = App.CurrentUser.Id;
            ((Frame)Window.Current.Content).Navigate(typeof(CreateProtocolPage),App.CurrentRecipe);
        }

        private async void EditProtocolButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.CurrentUser.Privilege > 1)
            {
                var dialog = new MessageDialog("You are not allowed to Edit this recipe (insufficient Privilege)");
                var result1 = await dialog.ShowAsync();
                return;
            }
            if (App.CurrentUser.Privilege>0)
              if (App.CurrentRecipe.Lock != 0)
                if (App.CurrentRecipe.Owner != App.CurrentUser.Id)
                {
                    var dialog = new MessageDialog("Recipe is Locked by owner, cannot Edit");
                    var result1 = await dialog.ShowAsync();
                    return;
                }
            App.CurrentRecipe.Tag = "Edit";
            ((Frame)Window.Current.Content).Navigate(typeof(CreateProtocolPage),App.CurrentRecipe);

        }
    }
}
