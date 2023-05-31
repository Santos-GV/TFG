using System;
using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Controllers;
using WpfAppTFG.Model;
using WpfAppTFG.Views.Controls;

namespace WpfAppTFG.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para PostPage.xaml
    /// </summary>
    public partial class PostPage : Page
    {
        public delegate void PostEliminadoEvento();
        public event PostEliminadoEvento postEliminadoEvento;
        private readonly PostController controller;
        private Post post;

        public PostPage(User user)
        {
            InitializeComponent();
            controller = new PostController(this, user);
        }

        public PostPage(Post post, User user) : this(user)
        {
            SetContext(post, user);
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
            postEliminadoEvento();
        }
    }
}
