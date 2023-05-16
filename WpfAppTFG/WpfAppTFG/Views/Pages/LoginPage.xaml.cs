using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using WpfAppTFG.Model;
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
            var user = login.logginUser;
            #if DEBUG
                user = new Model.User("debug", "debug") { Rol = Rol.Administrador};
            #endif
            if (user is null)
            {
                MessageBox.Show("No se pudo abrir la aplicación", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var homeWindow = new HomeWindow(user);
            WindowProperties.SetWindowOwner(homeWindow, this);
            homeWindow.Show();
        }
    }
}
