using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Lazy<IEnumerable<Post>>>> ReadAllPostPagedLazy()
        {
            const int pageSize = 20;
            var posts = await postRepository.ReadAllPagedLazy(pageSize);
            return posts;
        }
    }
}
