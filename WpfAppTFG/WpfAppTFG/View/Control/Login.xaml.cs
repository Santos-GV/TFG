using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Controller;

namespace WpfAppTFG.View.Control
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        private readonly LoginController controller;
        public Login()
        {
            InitializeComponent();
            controller = new LoginController();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            var isLogged = controller.Login(user.Text, psswd.Password);
            if (string.IsNullOrEmpty(user.Text) || string.IsNullOrEmpty(psswd.Password))
            {
                info.Text = "Rellena todos los campos";
                return;
            }
            if (isLogged.Result)
            {
                // TODO: Abrir ventana de la aplicación
            }
            else
            {
                info.Text = "El usuario y/o contraseña no son válidos";
                psswd.Password = string.Empty;
            }
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Abrir página de registro
        }
    }
}
