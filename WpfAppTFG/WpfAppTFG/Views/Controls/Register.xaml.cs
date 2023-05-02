using System.Windows.Controls;
using WpfAppTFG.Controller;

namespace WpfAppTFG.Views.Controls
{
    /// <summary>
    /// Lógica de interacción para Register.xaml
    /// </summary>
    public partial class Register : UserControl
    {
        public delegate void AtrasEvento();
        public event AtrasEvento atrasEvento;

        private readonly RegisterController registerController;

        public Register()
        {
            InitializeComponent();
            registerController = new RegisterController();
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
                atrasEvento();
            }
            else
            {
                info.Text = "No se a podido crear la cuenta usuario";
                return;
            }
        }

        private void atras_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            atrasEvento();
        }
    }
}
