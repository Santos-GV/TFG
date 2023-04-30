using System.Collections.Generic;
using System.Linq;
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
        /// Obtiene todos los posts de un <see cref="User"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IQueryable<Post> ReadAllPost(int userId);

        /// <summary>
        /// Obtiene todos los comentarios de un <see cref="User"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IQueryable<Comentario> ReadAllComentario(int userId);

        /// <summary>
        /// Obtiene todos los logs de un <see cref="User"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IQueryable<Log> ReadAllLogs(int userId);
    }
}
