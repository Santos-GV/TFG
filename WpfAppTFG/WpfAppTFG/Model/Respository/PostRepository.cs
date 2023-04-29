using System.Collections.Generic;
using System.Threading.Tasks;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model.Respository
{
    /// <summary>
    /// Implementación de un Repositorio de <see cref="Post"/>
    /// </summary>
    public class PostRepository : IRepository<Post>
    {
        private readonly IDAO<Post> postDAO;

        public PostRepository(IDAO<Post> postDAO)
        {
            this.postDAO = postDAO;
        }

        /// <summary>
        /// Crea un <see cref="Post"/>
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task Create(Post post)
        {
            await postDAO.Create(post);
        }

        /// <summary>
        /// Elimina un <see cref="Post"/>
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task Delete(Post post)
        {
            await postDAO.Create(post);
        }

        /// <summary>
        /// Obtiene un <see cref="Post"/> por su id
        /// </summary>
        /// <remarks>Puede ser nulo, si no existe</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Post?> Read(int id)
        {
            var post = await postDAO.Read(id);
            return post;
        }

        /// <summary>
        /// Obtiene todos los <see cref="Post"/>
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Post>> ReadAll()
        {
            var posts = await postDAO.ReadAll();
            return posts;
        }

        /// <summary>
        /// Actualiza un <see cref="Post"/>
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task Update(Post post)
        {
            await postDAO.Update(post);
        }
    }
}
