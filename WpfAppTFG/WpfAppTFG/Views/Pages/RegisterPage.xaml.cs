using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Views.Shareds;

namespace WpfAppTFG.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void regiter_Loaded(object sender, RoutedEventArgs e)
        {
            WindownProperties.SetWindowTitle("Register", this);
        }

        private void regiter_atrasEvento()
        {
            var loginPage = new LoginPage();
            this.NavigationService.Navigate(loginPage);
        }
    }
}
