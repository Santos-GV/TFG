using System.Collections.Generic;
using System.Threading.Tasks;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model.Respository
{
    /// <summary>
    /// Implementación de un Repositorio de <see cref="Post"/>
    /// </summary>
    public class PostRepository : IPostRepository
    {
        private IDAO<Post> postDAO;

        public PostRepository(IDAO<Post> postDAO)
        {
            this.postDAO = postDAO;
        }

        public Task Create(Post entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(Post entity)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Obtiene todos los posts
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Post>> GetAllPost()
        {
            var posts = await postDAO.ReadAll();
            return posts;
        }

        /// <summary>
        /// Obtiene un post por su id
        /// Puede ser nulo, si no existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Post?> GetPost(int id)
        {
            var post = await postDAO.Read(id);
            return post;
        }

        public Task<Post?> Read(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Post>> ReadAll()
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Post entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
