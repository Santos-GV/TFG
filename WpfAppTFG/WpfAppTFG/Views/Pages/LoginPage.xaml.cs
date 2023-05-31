using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Controller;
using WpfAppTFG.Model;
using WpfAppTFG.Model.Respository;
using WpfAppTFG.Views.Shareds;
using WpfAppTFG.Views.Windows;

namespace WpfAppTFG.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private readonly LoginController controller;
        public User? logginUser;

        public LoginPage()
        {
            InitializeComponent();
            controller = new LoginController();
        }
        private void login_Loaded(object sender, RoutedEventArgs e)
        {
            WindowProperties.SetWindowTitle("Login", this);
        }

        private void RegistrarseEvento()
        {
            var registerPage = new RegisterPage();
            this.NavigationService.Navigate(registerPage);
        }

        private void LoginEvento()
        {
            WindowProperties.SetWindowVisibility(Visibility.Hidden, this);
            var user = logginUser;
            if (user is null)
            {
                MessageBox.Show("No se pudo abrir la aplicación", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var homeWindow = new HomeWindow(user);
            WindowProperties.SetWindowOwner(homeWindow, this);
            homeWindow.Show();
        }

        private async void login_Click(object sender, RoutedEventArgs e)
        {
            var userTask = controller.Login(userName.Text, psswd.Password);
            if (string.IsNullOrEmpty(userName.Text) || string.IsNullOrEmpty(psswd.Password))
            {
                info.Text = "Rellena todos los campos";
                return;
            }
            var user = await userTask;
            if (user is not null)
            {
                logginUser = user;
                info.Text = string.Empty;
                userName.Text = string.Empty;
                psswd.Password = string.Empty;
                LoginEvento();
            }
            else
            {
                info.Text = "El usuario y/o contraseÃ±a no son vÃ¡lidos";
                psswd.Password = string.Empty;
            }
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            RegistrarseEvento();
        }
    }
}
