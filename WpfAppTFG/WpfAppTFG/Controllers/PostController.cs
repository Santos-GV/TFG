using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfAppTFG.Model;
using WpfAppTFG.Model.Respository;

namespace WpfAppTFG.Controllers
{
    public class PostController
    {
        private readonly PostRepository postRepository;
        private readonly UserRepository userRepository;
        private readonly User user;

        public PostController(User user)
        {
            this.postRepository = new PostRepository();
            this.userRepository = new UserRepository();
            this.user = user;
        }

        public async Task<IEnumerable<Lazy<IEnumerable<Post>>>> ReadAllPostPagedLazy()
        {
            const int pageSize = 20;
            var posts = await postRepository.ReadAllPagedLazy(pageSize);
            return posts;
        }

        internal async Task addFavoritos(Post post)
        {
            if (user.Favoritos.Contains(post)) return;
            user.Favoritos.Add(post);
            await userRepository.Update(user);
        }

        internal async Task addPendientes(Post post)
        {
            if (user.Pendientes.Contains(post)) return;
            user.Pendientes.Add(post);
            await userRepository.Update(user);
        }
    }
}
