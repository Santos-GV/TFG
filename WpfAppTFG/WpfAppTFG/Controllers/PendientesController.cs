using System.Collections.Generic;
using System.Threading.Tasks;
using WpfAppTFG.Model.Respository;
using WpfAppTFG.Model;
using System.Linq;

namespace WpfAppTFG.Controllers
{
    public class PendientesController
    {
        private readonly User user;
        private readonly PostRepository postRepository;
        private readonly UserRepository userRepository;

        public PendientesController(User user)
        {
            this.user = user;
            this.postRepository = new PostRepository(user);
            this.userRepository = new UserRepository();
        }

        internal IEnumerable<Post> ReadAllPost()
        {
            var posts = this.user.Pendientes
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
