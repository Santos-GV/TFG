using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfAppTFG.Model;
using WpfAppTFG.Model.Respository;

namespace WpfAppTFG.Controllers
{
    public class FavoritosController
    {
        private readonly User user;
        private readonly PostRepository postRepository;
        private readonly UserRepository userRepository;

        public FavoritosController(User user)
        {
            this.user = user;
            this.postRepository = new PostRepository();
            this.userRepository = new UserRepository();
        }

        internal IEnumerable<Post> ReadAllPost()
        {
            var posts = this.user.Favoritos
                .Select(postId => postRepository.Read(postId))
                .OfType<Post>(); // Filtra los nulls
            return posts;
        }

        internal async Task addFavoritos(Post post)
        {
            if (user.Favoritos.Contains(post.Id)) return;
            user.Favoritos.Add(post.Id);
            await userRepository.Update(user);
        }

        internal async Task addPendientes(Post post)
        {
            if (user.Pendientes.Contains(post.Id)) return;
            user.Pendientes.Add(post.Id);
            await userRepository.Update(user);
        }
    }
}
