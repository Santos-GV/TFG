using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Controller;

namespace WpfAppTFG.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private readonly LoginController controller;

        public LoginPage()
        {
            InitializeComponent();
            controller = new LoginController(this);
        }
        private void login_Loaded(object sender, RoutedEventArgs e)
        {
            controller.ChangeWindowTitle();
        }

        private async void login_Click(object sender, RoutedEventArgs e)
        {
            await controller.TryLogin();
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            controller.Registrarse();
        }
    }
}
