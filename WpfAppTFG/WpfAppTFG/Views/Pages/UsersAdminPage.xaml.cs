using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Controllers;
using WpfAppTFG.Model;

namespace WpfAppTFG.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para UsersAdminPage.xaml
    /// </summary>
    public partial class UsersAdminPage : Page
    {
        private readonly UsersAdminController controller;

        public UsersAdminPage()
        {
            InitializeComponent();
            controller = new UsersAdminController(this);
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            controller.Buscar();
        }

        private async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            await controller.Eliminar();
        }

        private async void rol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await controller.CambiarRol();
        }
    }
}
