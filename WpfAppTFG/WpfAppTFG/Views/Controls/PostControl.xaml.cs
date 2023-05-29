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
            controller = new PostController(this);
        }

        public void SetContext(Post post, User user)
        {
            this.post = post;
            controller.SetContext(post, user);
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
            foreach (var comentario in post.Comentarios)
            {
                var control = new ComentarioControl(comentario, user);
                control.Margin = new Thickness(8);

                stpComentarios.Children.Add(control);
            }
        }

        private void btnComentar_Click(object sender, RoutedEventArgs e)
        {
            controller.AddComentario();
        }

        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            await controller.EliminarPost(post);
        }
    }
}
