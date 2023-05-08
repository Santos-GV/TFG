using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Controllers;
using WpfAppTFG.Model;
using WpfAppTFG.Views.Shareds;

namespace WpfAppTFG.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para UsersAdminPage.xaml
    /// </summary>
    public partial class UsersAdminPage : Page
    {
        private readonly UsersAdminController controller;
        private User? user;

        public UsersAdminPage()
        {
            InitializeComponent();
            controller = new UsersAdminController();
            rol.ItemsSource = Enum.GetValues<Rol>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            WindowProperties.SetWindowTitle("Administración de usuarios", this);
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            var userName = nombreBusqueda.Text;
            user = controller.SearchUser(userName).Result;
            if (user == null)
            {
                info.Content = "El usuario no existe";
                return;
            }
            nombre.Text = user.Name;
            rol.SelectedItem = user.Rol;
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            const string message = 
@"¿Estás seguro?
Está accicion no se puede deshacer";
            var result = MessageBox.Show(message, "Eliminar usuario", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.No) return;
            if (user == null) return;
            controller.Delete(user).Wait();
        }

        private void rol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRol = rol.SelectedItem.ToString();
            if (selectedRol == null) return;
            var isParsed = Enum.TryParse(selectedRol, out Rol newRol);
            if (!isParsed) return;
            if (user == null) return;
            user.Rol = newRol;
        }
    }
}
