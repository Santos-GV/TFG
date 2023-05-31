using System.Threading.Tasks;
using System.Windows;
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
        private readonly PostPage view;
        private Post post;
        private User user;

        public PostController(User user)
        {
            this.postRepository = new PostRepository(user);
            this.comentarioRepository = new ComentarioRepository(user);
        }
        public PostController(PostPage view, User user) : this(user)
        {
            this.view = view;
        }

        internal void AddComentario()
        {
            var comentario = new Comentario(user.Id, view.txtBoxComentario.Text);
            comentarioRepository.Create(post.Id, comentario);
            MessageBox.Show($"Comentario creado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

            view.txtBoxComentario.Text = string.Empty;

            var control = new ComentarioControl(comentario, user);
            control.Margin = new Thickness(8);

            view.stpComentarios.Children.Add(control);
        }

        internal async Task EliminarPost(Post post)
        {
            var result = MessageBox.Show($"Quiere eliminar el Post `{post.Titulo}`?", "Peligro", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No) return;
            await postRepository.Delete(post);
            MessageBox.Show($"Post `{post.Titulo} eliminado`", "Peligro", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        internal void SetContext(Post post, User user)
        {
            this.post = post;
            this.user = user;
        }
    }
}
