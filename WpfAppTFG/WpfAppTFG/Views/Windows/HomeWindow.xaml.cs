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
    }
}
