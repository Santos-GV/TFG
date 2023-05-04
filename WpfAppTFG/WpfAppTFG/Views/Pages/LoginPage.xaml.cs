using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using WpfAppTFG.Views.Shareds;
using WpfAppTFG.Views.Windows;

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

        private void login_Loaded(object sender, RoutedEventArgs e)
        {
            WindowProperties.SetWindowTitle("Login", this);
        }

        private void login_registrarseEvento()
        {
            var registerPage = new RegisterPage();
            this.NavigationService.Navigate(registerPage);
        }

        private void login_loginEvento()
        {
            WindowProperties.SetWindowVisibility(Visibility.Hidden, this);
            var homeWindow = new HomeWindow();
            WindowProperties.SetWindowOwner(homeWindow, this);
            homeWindow.Show();
        }
    }
}
