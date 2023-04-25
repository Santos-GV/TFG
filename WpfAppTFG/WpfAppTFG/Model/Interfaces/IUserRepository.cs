using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfAppTFG.Model.Interfaces
{
    /// <summary>
    /// Representación abstracta de un Repositorio de <see cref="User"/>
    /// </summary>
    /// <remarks>
    /// Se diferencia en un Repository en que este
    /// tiene funcionalidades concretas de los <see cref="User"/>
    /// </remarks>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Obtiene todos los posts de un usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<Post>> GetAllPost(int userId);

        /// <summary>
        /// Obtiene todos los comentarios de un usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<Comentario>> GetAllComentario(int userId);
    }
}
