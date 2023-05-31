using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Controller;
using WpfAppTFG.Views.Shareds;

namespace WpfAppTFG.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private readonly RegisterController registerController;
        public RegisterPage()
        {
            InitializeComponent();
            registerController = new RegisterController();
        }

        private void regiter_Loaded(object sender, RoutedEventArgs e)
        {
            WindowProperties.SetWindowTitle("Register", this);
        }
        private void AtrasEvento()
        {
            var loginPage = new LoginPage();
            this.NavigationService.Navigate(loginPage);
        }

        private void registrase_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var userName = user.Text;
            var psswd1 = this.psswd1.Password;
            var psswd2 = this.psswd2.Password;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(psswd1) || string.IsNullOrEmpty(psswd2))
            {
                info.Text = "Rellena todos los campos";
                return;
            }
            if (!psswd1.Equals(psswd2))
            {
                info.Text = "Las contraseñas no coinciden";
                return;
            }
            var isRegistered = registerController.Register(userName, psswd1).Result;
            if (isRegistered)
            {
                AtrasEvento();
            }
            else
            {
                info.Text = "No se a podido crear la cuenta usuario";
                return;
            }
        }

        private void atras_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AtrasEvento();
        }
    }
}
