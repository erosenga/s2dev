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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace S2App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateProtocolPage : Page
    {
        public CreateProtocolPage()
        {
            this.InitializeComponent();
            if (App.CurrentUser.Privilege > 0)
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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Recipe recipe = e.Parameter as Recipe;

            if (recipe != null)
            {
                App.CurrentRecipe = recipe;
                Protocol.Text = recipe.NickName;
                Species.Text = recipe.Species;
                Tissue.Text = recipe.Organ;
                DisruptionTime.Value = recipe.GrindTime;
                DisruptionTemp.Value = recipe.GrindTemp;
                DisruptionSpeed.Value = recipe.RPM;
                IncubationTime.Value = recipe.IncubationTime;
                IncubationTemperature.Value = recipe.IncubationTemp;
                Cycles.Value = recipe.Cycles;
                Enzyme.Value = recipe.Enzyme;
                State.SelectedIndex = (int) recipe.State;
                Type.SelectedIndex = (int) recipe.Type;
                Lock.IsChecked = (recipe.Lock != 0);
                Notes.Text = recipe.Comment;
                if(recipe.Tag=="Edit")
                {
                    CreateProtocolBanner.Text = "Edit Protocol";
                }
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

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
        }

        private void SaveProtocolButton_Click(object sender, RoutedEventArgs e)
        {
            Recipe localrecipe = new Recipe();
        
            localrecipe.NickName = Protocol.Text;
            localrecipe.Lock = (bool)Lock.IsChecked?1:0;
            localrecipe.Species = Species.Text;
            localrecipe.IncubationTime =  (long)IncubationTime.Value;
            localrecipe.GrindTime = (long)DisruptionTime.Value;
            localrecipe.Cycles = (long)Cycles.Value;
            localrecipe.Organ = Tissue.Text;
            localrecipe.IncubationTemp = (long)IncubationTemperature.Value;
            localrecipe.RPM = (long)DisruptionSpeed.Value;
            localrecipe.Enzyme = (long)Enzyme.Value;
            localrecipe.State = State.SelectedIndex;
            localrecipe.Type = Type.SelectedIndex;
            localrecipe.Tag = "Insert";
            localrecipe.Comment = Notes.Text;
            localrecipe.Owner=App.CurrentUser.Id;
            App.CurrentRecipe = localrecipe;
            DataAccess.maintainRecipe(localrecipe);
            ((Frame)Window.Current.Content).Navigate(typeof(ViewProtocolPage),App.CurrentRecipe);
        }
    }
}
                
        
                   