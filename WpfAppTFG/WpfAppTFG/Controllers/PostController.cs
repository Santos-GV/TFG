using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Model;
using WpfAppTFG.Model.Respository;
using WpfAppTFG.Views.Controls;
using WpfAppTFG.Views.Pages;

namespace WpfAppTFG.Controllers
{
    public class PostController
    {
        private readonly PostRepository postRepository;
        private readonly ComentarioRepository comentarioRepository;
        private readonly LogRepository logRepository;
        private readonly PostPage view;
        private Post post;
        private User user;

        public PostController()
        {
            this.postRepository = new PostRepository();
            this.comentarioRepository = new ComentarioRepository();
            this.logRepository = new LogRepository();
        }
        public PostController(PostPage view) : this()
        {
            this.view = view;
        }

        public async Task AddComentario()
        {
            var comentario = new Comentario(user.Id, view.txtBoxComentario.Text);
            comentarioRepository.Create(post.Id, comentario);
            var log = new Log(user.Id, $"Crea el comentario `{comentario.Id}`");
            await logRepository.Create(log);
            MessageBox.Show($"Comentario creado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

            view.txtBoxComentario.Text = string.Empty;

            var control = new ComentarioControl(comentario, user);
            control.Margin = new Thickness(8);

            view.stpComentarios.Children.Add(control);
        }

        public async Task EliminarPost()
        {
            var result = MessageBox.Show($"Quiere eliminar el Post `{post.Titulo}`?", "Peligro", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No) return;
            var postTask = postRepository.Delete(post);
            var log = new Log(user.Id, $"Elimina el post `{post.Id}`");
            var logTask = logRepository.Create(log);
            await Task.WhenAll(postTask, logTask);
            MessageBox.Show($"Post `{post.Titulo} eliminado`", "Peligro", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void SetContext(Post post, User user)
        {
            this.post = post;
            this.user = user;
            view.txtTitulo.Text = post.Titulo;
            view.txtContenido.Text = post.Contenido;
            view.btnEliminar.Visibility = user.Rol switch
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
                control.Style = (Style)view.Resources["text-block"];

                view.stpEtiquetas.Children.Add(control);
            }
            foreach (var comentario in post.Comentarios)
            {
                var control = new ComentarioControl(comentario, user);
                control.Margin = new Thickness(8);

                view.stpComentarios.Children.Add(control);
            }
        }
    }
}
