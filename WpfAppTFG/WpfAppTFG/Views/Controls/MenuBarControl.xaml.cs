using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Controllers;
using WpfAppTFG.Model;

namespace WpfAppTFG.Views.Controls
{
    /// <summary>
    /// Lógica de interacción para MenuBar.xaml
    /// </summary>
    public partial class MenuBarControl : UserControl
    {
        public delegate void CrearPostEvento();
        public event CrearPostEvento crearPostEvento;
        public delegate void MisPostEvento();
        public event MisPostEvento misPostEvento;
        public delegate void CerrarSesionEvento();
        public event CerrarSesionEvento cerrarSesionEvento;
        public delegate void SalirEvento();
        public event SalirEvento salirEvento;
        public delegate void PostsEvento();
        public event PostsEvento postsEvento;
        public delegate void FavoritosEvento();
        public event FavoritosEvento favoritosEvento;
        public delegate void PendientesEvento();
        public event PendientesEvento pendientesEvento;
        public delegate void AdministrarUsuariosEvento();
        public event AdministrarUsuariosEvento administrarUsuariosEvento;
        public delegate void AyudaEvento();
        public event AyudaEvento ayudaEvento;
        public delegate void AcercaDeEvento();
        public event AcercaDeEvento acercaDeEvento;
        private readonly MenuBarController controller;

        public MenuBarControl()
        {
            InitializeComponent();
            controller = new MenuBarController(this);
        }

        public void SetContext(User user)
        {
            controller.SetContext(user);

        }

        private void cerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            cerrarSesionEvento();
        }

        private void salir_Click(object sender, RoutedEventArgs e)
        {
            salirEvento();
        }

        private void Posts_Click(object sender, RoutedEventArgs e)
        {
            postsEvento();
        }

        private void Favoritos_Click(object sender, RoutedEventArgs e)
        {
            favoritosEvento();
        }

        private void Pendientes_Click(object sender, RoutedEventArgs e)
        {
            pendientesEvento();
        }

        private void administrarUsuarios_Click(object sender, RoutedEventArgs e)
        {
            administrarUsuariosEvento();
        }

        private void ayuda_Click(object sender, RoutedEventArgs e)
        {
            ayudaEvento();
        }

        private void acerdaDe_Click(object sender, RoutedEventArgs e)
        {
            acercaDeEvento();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            crearPostEvento();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            misPostEvento();
        }

        public void UpdateNotificaciones(bool hasNotifications = false)
        {
            controller.UpdateNotificaciones(hasNotifications);
        }
    }
}
