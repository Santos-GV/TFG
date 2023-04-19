using System.Windows;
using System.Windows.Controls;

namespace WpfAppTFG.View.Control
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(user.Text) || string.IsNullOrEmpty(psswd.Password))
            {
                info.Text = "Rellena todos los campos";
            }
            // TODO: Enviar al controlador los datos del formulario
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Enviar al controalador
        }
    }
}
