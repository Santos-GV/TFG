using System.Windows;
using WpfAppTFG.Views.Pages;

namespace WpfAppTFG.Views.Windows
{
    /// <summary>
    /// Lógica de interacción para HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
        }

        private void MenuBar_administrarUsuarios()
        {
            var usersAdminPage = new UsersAdminPage();
            pagesContainer.Navigate(usersAdminPage);
        }
    }
}
