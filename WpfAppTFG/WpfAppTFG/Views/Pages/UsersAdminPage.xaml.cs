using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Views.Shareds;

namespace WpfAppTFG.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para UsersAdminPage.xaml
    /// </summary>
    public partial class UsersAdminPage : Page
    {
        public UsersAdminPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            WindowProperties.SetWindowTitle("Administración de usuarios", this);
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
