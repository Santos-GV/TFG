using System;
using System.Threading.Tasks;
using System.Windows;
using WpfAppTFG.Model;
using WpfAppTFG.Model.Respository;

namespace WpfAppTFG.Controllers
{
    public class PostController
    {
        private readonly PostRepository postRepository;

        public PostController()
        {
            this.postRepository = new PostRepository();
        }

        internal async Task EliminarPost(Post post)
        {
            var result = MessageBox.Show($"Quiere eliminar el Post `{post.Titulo}`?", "Peligro", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No) return;
            await postRepository.Delete(post);
            MessageBox.Show($"Post `{post.Titulo} eliminado`", "Peligro", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
