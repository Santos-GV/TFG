using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfAppTFG.Model.Interfaces
{
    /// <summary>
    /// Representación abstracta de un Repositorio de <see cref="Post"/>
    /// </summary>
    /// <remarks>
    /// Se diferencia en un Repository en que este
    /// tiene funcionalidades concretas de los <see cref="Post"/>
    /// </remarks>
    public interface IPostRepository : IRepository<Post>
    {
        /// <summary>
        /// Obtiene un post por su id
        /// Puede ser nulo, si no existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Post?> GetPost(int id);

        /// <summary>
        /// Obtiene todos los posts
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<Post>> GetAllPost();
    }
}
