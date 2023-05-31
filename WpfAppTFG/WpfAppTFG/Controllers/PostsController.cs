using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfAppTFG.Model;
using WpfAppTFG.Model.Respository;

namespace WpfAppTFG.Controllers
{
    public class PostsController
    {
        private readonly PostRepository postRepository;
        private readonly UserRepository userRepository;
        private readonly User user;

        public PostsController(User user)
        {
            this.postRepository = new PostRepository(user);
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
