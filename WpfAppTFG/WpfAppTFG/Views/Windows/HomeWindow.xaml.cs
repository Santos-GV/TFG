using System;
using System.Windows;
using WpfAppTFG.Model;
using WpfAppTFG.Views.Controls;
using WpfAppTFG.Views.Pages;

namespace WpfAppTFG.Views.Windows
{
    /// <summary>
    /// Lógica de interacción para HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private User user;

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
            this.user = user;
            menu = new MenuBar(user);
        }

        private void menu_postsEvento()
        {
            var postsPage = new PostsPage();
            pagesContainer.Navigate(postsPage);
        }

        private void MenuBar_administrarUsuarios()
        {
            var usersAdminPage = new UsersAdminPage();
            pagesContainer.Navigate(usersAdminPage);
        }

        private void MenuBar_acercaDeEvento()
        {
            var acerdaDeWindow = new AcercaDeWindow();
            acerdaDeWindow.Owner = this;
            acerdaDeWindow.ShowDialog();
        }

        private void MenuBar_salirEvento()
        {
            // Cierra la aplicación entera
            // tanto la ventana padre, como esta
            Application.Current.Shutdown();
        }

        private void MenuBar_cerrarSesionEvento()
        {
            try
            {
                Owner.Visibility = Visibility.Visible;
                this.Close();
            }
            catch (InvalidOperationException) { }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Al cerrar la ventana se cierra sesión
            MenuBar_cerrarSesionEvento();
        }
    }
}
