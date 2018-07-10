using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace S2App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InsertSamplePage : Page
    {
        public InsertSamplePage()
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

        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(WelcomeUserPage));
        }

        private void ButtonProtocols_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(RecipeGrid1));
        }

        private void ButtonSignOut_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentUser = null;
            ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearNotesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
                return;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonAdministration_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentUser = null;
            ((Frame)Window.Current.Content).Navigate(typeof(AdministrationPage));
        }
        
        private void ButtonRunLog_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
