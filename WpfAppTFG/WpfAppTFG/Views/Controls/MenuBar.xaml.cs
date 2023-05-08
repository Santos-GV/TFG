using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfAppTFG.Model;

namespace WpfAppTFG.Views.Controls
{
    /// <summary>
    /// Lógica de interacción para MenuBar.xaml
    /// </summary>
    public partial class MenuBar : UserControl
    {
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
        private User user;

        public MenuBar()
        {
            InitializeComponent();
        }

        public MenuBar(User user) : this()
        {
            // TODO: check visibility at runtime
            // Parece funcionar solo en el constructor por defecto
            if (user.Rol.Equals(Rol.Administrador))
            {
                administrarUsuarios.Visibility = Visibility.Visible;
            };
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
    }
}
