using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Controller;
using WpfAppTFG.Model;

namespace WpfAppTFG.Views.Controls
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public delegate void RegistrarseEvento();
        public event RegistrarseEvento registrarseEvento;
        public delegate void LoginEvento();
        public event LoginEvento loginEvento;
        public User? logginUser; 

        private readonly LoginController controller;
        public Login()
        {
            InitializeComponent();
            controller = new LoginController();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            #if DEBUG
                loginEvento();
                return;
            #endif
            var isLogged = controller.Login(userName.Text, psswd.Password);
            if (string.IsNullOrEmpty(userName.Text) || string.IsNullOrEmpty(psswd.Password))
            {
                info.Text = "Rellena todos los campos";
                return;
            }
            if (isLogged.Result is not null)
            {
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
