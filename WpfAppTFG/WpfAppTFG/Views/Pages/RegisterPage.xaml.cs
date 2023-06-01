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
        private readonly RegisterController controller;
        public RegisterPage()
        {
            InitializeComponent();
            controller = new RegisterController(this);
        }

        private void regiter_Loaded(object sender, RoutedEventArgs e)
        {
            controller.ChangeWindowTitle();
        }

        private async void registrase_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await controller.Register();
        }

        private void atras_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            controller.NavigateLogin();
        }
    }
}
