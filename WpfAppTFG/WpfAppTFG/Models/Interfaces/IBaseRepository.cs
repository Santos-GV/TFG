using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WpfAppTFG.Model.Interfaces
{
    /// <summary>
    /// Representación abstracta de un Repositorio base para otros repositorios
    /// </summary>
    /// <remarks>No tiene Create</remarks>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T>
    where T : IIdentifiable
    {
        /// <summary>
        /// Obtiene un <see cref="T"/>
        /// </summary>
        /// <remarks>
        /// Puede ser nulo, si no existe
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T?> Read(string id);

        /// <summary>
        /// Obtiene todos los <see cref="T"/>
        /// </summary>
        /// <returns></returns>
        IMongoQueryable<T> ReadAll();

        /// <summary>
        /// Actualiza un objeto <see cref="T"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Update(T entity);

        /// <summary>
        /// Elimina un objeto <see cref="T"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Delete(T entity);
    }
}
