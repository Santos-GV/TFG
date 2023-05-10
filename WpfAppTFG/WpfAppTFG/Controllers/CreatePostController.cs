using System.Collections.Generic;
using System.Threading.Tasks;
using WpfAppTFG.Model;
using WpfAppTFG.Model.Respository;

namespace WpfAppTFG.Controllers
{
    public class CreatePostController
    {
        private readonly PostRepository postRepository;
        private readonly User user;

        public CreatePostController(User user)
        {
            this.postRepository = new PostRepository();
            this.user = user;
        }

        public async Task CreatePost(string title, string content, List<string> tags)
        {
            var post = new Post(user.GetId(), title, content, tags);
            await postRepository.Create(post);
        }
    }
}
