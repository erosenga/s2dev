using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
            ((Frame)Window.Current.Content).Navigate(typeof(RecipeGrid1));
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
        //read back UI fields into recipe; filter quotes to prevent SQL errors (replace quotes by quote-quote)
            localrecipe.NickName = App.QuoteFilter(Protocol.Text);
            localrecipe.Lock = (bool)Lock.IsChecked?1:0;
            localrecipe.Species = App.QuoteFilter(Species.Text);
            localrecipe.IncubationTime =  (long)IncubationTime.Value;
            localrecipe.GrindTime = (long)DisruptionTime.Value;
            localrecipe.Cycles = (long)Cycles.Value;
            localrecipe.Organ = App.QuoteFilter(Tissue.Text);
            localrecipe.IncubationTemp = (long)IncubationTemperature.Value;
            localrecipe.RPM = (long)DisruptionSpeed.Value;
            localrecipe.Enzyme = (long)Enzyme.Value;
            localrecipe.GrindTemp = (long)DisruptionTemp.Value;
            localrecipe.State = State.SelectedIndex;
            localrecipe.Type = Type.SelectedIndex;
            localrecipe.Tag = "Insert";
            localrecipe.Comment = App.QuoteFilter(Notes.Text);
            localrecipe.Owner=App.CurrentUser.Id;
            //insert recipe into SQL database
            DataAccess.maintainRecipe(localrecipe);
            //read back UI fields into global CurrentRecipe; Do not filter out quotes
            App.CurrentRecipe.NickName = (Protocol.Text);
            App.CurrentRecipe.Lock = (bool)Lock.IsChecked ? 1 : 0;
            App.CurrentRecipe.Species = (Species.Text);
            App.CurrentRecipe.IncubationTime = (long)IncubationTime.Value;
            App.CurrentRecipe.GrindTime = (long)DisruptionTime.Value;
            App.CurrentRecipe.Cycles = (long)Cycles.Value;
            App.CurrentRecipe.Organ = (Tissue.Text);
            App.CurrentRecipe.IncubationTemp = (long)IncubationTemperature.Value;
            App.CurrentRecipe.RPM = (long)DisruptionSpeed.Value;
            App.CurrentRecipe.Enzyme = (long)Enzyme.Value;
            App.CurrentRecipe.GrindTemp = (long)DisruptionTemp.Value;
            App.CurrentRecipe.State = State.SelectedIndex;
            App.CurrentRecipe.Type = Type.SelectedIndex;
            App.CurrentRecipe.Tag = "Insert";
            App.CurrentRecipe.Comment = (Notes.Text);
            App.CurrentRecipe.Owner = App.CurrentUser.Id;
            
            ((Frame)Window.Current.Content).Navigate(typeof(ViewProtocolPage),App.CurrentRecipe);
        }
    }
}
                
        
                   