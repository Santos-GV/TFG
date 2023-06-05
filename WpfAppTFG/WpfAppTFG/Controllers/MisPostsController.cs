using WpfAppTFG.Model.Respository;
using WpfAppTFG.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WpfAppTFG.Controllers
{
    public class MisPostsController
    {
        private readonly User user;
        private readonly PostRepository postRepository;
        private readonly UserRepository userRepository;

        public MisPostsController(User user)
        {
            this.user = user;
            this.postRepository = new PostRepository();
            this.userRepository = new UserRepository();
        }

        public IEnumerable<Post> ReadAllPost()
        {
            var posts = postRepository.ReadAll()
                .Where(post => post.IdUsuario.Equals(user.Id));
            return posts;
        }

        public async Task addFavoritos(Post post)
        {
            if (user.Favoritos.Contains(post.Id)) return;
            user.Favoritos.Add(post.Id);
            await userRepository.Update(user);
        }

        public async Task addPendientes(Post post)
        {
            if (user.Pendientes.Contains(post.Id)) return;
            user.Pendientes.Add(post.Id);
            await userRepository.Update(user);
        }
    }
}
