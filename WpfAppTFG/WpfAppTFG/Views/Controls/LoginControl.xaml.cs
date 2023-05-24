using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Controller;
using WpfAppTFG.Model;

namespace WpfAppTFG.Views.Controls
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        public delegate void RegistrarseEvento();
        public event RegistrarseEvento registrarseEvento;
        public delegate void LoginEvento();
        public event LoginEvento loginEvento;
        public User? logginUser; 

        private readonly LoginController controller;
        public LoginControl()
        {
            InitializeComponent();
            controller = new LoginController();
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
                loginEvento();
            }
            else
            {
                info.Text = "El usuario y/o contraseña no son válidos";
                psswd.Password = string.Empty;
            }
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            registrarseEvento();
        }
    }
}
