using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using WpfAppTFG.Views.Shareds;

namespace WpfAppTFG.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            WindownProperties.SetWindowTitle("Login", this);
        }

        private void login_registrarseEvento()
        {
            var registerPage = new RegisterPage();
            this.NavigationService.Navigate(registerPage);
        }
    }
}
