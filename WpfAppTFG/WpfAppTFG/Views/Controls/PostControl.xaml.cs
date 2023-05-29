using System;
using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Controllers;
using WpfAppTFG.Model;

namespace WpfAppTFG.Views.Controls
{
    /// <summary>
    /// Lógica de interacción para PostControl.xaml
    /// </summary>
    public partial class PostControl : UserControl
    {
        private readonly PostController controller;
        private Post post;

        public PostControl()
        {
            InitializeComponent();
            controller = new PostController();
        }

        public void SetContext(Post post, User user)
        {
            this.post = post;
            txtTitulo.Text = post.Titulo;
            txtContenido.Text = post.Contenido;
            btnEliminar.Visibility = user.Rol switch
            {
                Rol.Regular => Visibility.Collapsed,
                Rol.Moderador => Visibility.Visible,
                Rol.Administrador => Visibility.Visible,
                _ => throw new ArgumentOutOfRangeException($"Rol de usuario inesperado `{user.Rol}`")
            };
            foreach (var etiqueta in post.Etiquetas)
            {
                var control = new TextBlock();
                control.Text = etiqueta;
                control.Margin = new Thickness(8);
                control.Style = (Style)Resources["text-block"];

                stpEtiquetas.Children.Add(control);
            }
        }

        private void btnVerComentarios_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void btnComentar_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            await controller.EliminarPost(post);
        }
    }
}
