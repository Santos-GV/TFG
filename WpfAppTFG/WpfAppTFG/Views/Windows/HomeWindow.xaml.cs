using System;
using System.Windows;
using System.Windows.Navigation;
using WpfAppTFG.Controllers;
using WpfAppTFG.Model;

namespace WpfAppTFG.Views.Windows
{
    /// <summary>
    /// Lógica de interacción para HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private readonly HomeController controller;

        public HomeWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Crea la ventana home
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="ArgumentOutOfRangeException">Rol de usuario inesperado</exception>
        public HomeWindow(User user) : this()
        {
            controller = new HomeController(this, user);
        }

        private void pagesContainer_Navigated(object sender, NavigationEventArgs e)
        {
            controller.PageNavigated();
        }

        private void menu_postsEvento()
        {
            controller.NavigatePosts();
        }

        private void MenuBar_administrarUsuarios()
        {
            controller.NavigateAdministrarUsuarios();
        }

        private void MenuBar_acercaDeEvento()
        {
            controller.NavigateAcercaDe();
        }

        private void MenuBar_salirEvento()
        {
            controller.Salir();
        }

        private void MenuBar_cerrarSesionEvento()
        {
            controller.CerrarSesion();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Al cerrar la ventana se cierra sesión
            MenuBar_cerrarSesionEvento();
        }

        private void menu_crearPostEvento()
        {
            controller.NavigateCrearPost();
        }

        private void menu_favoritosEvento()
        {
            controller.NavigateFavoritos();
        }

        private void menu_pendientesEvento()
        {
            controller.NavigatePendientes();
        }

        private void menu_misPostEvento()
        {
            controller.NavigateMisPosts();
        }

        private void AtajoCrearPost(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            controller.NavigateCrearPost();
        }
        private void AtajoMisPosts(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            controller.NavigateMisPosts();
        }

        private void AtajoCerrarSesion(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            controller.CerrarSesion();
        }

        private void AtajoSalir(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            controller.Salir();
        }

        private void AtajoPosts(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            controller.NavigatePosts();
        }
        private void AtajoFavoritos(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            controller.NavigateFavoritos();
        }
        private void AtajoPendientes(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            controller.NavigatePendientes();
        }
        private void AtajoAyuda(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException("TODO: Añadir venta de Acerca de");
        }
        private void AtajoAcercaDe(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            controller.NavigateAcercaDe();
        }
    }
}
