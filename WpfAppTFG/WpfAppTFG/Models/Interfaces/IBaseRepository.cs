using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfAppTFG.Model.Interfaces
{
    /// <summary>
    /// Representación abstracta de un Repositorio base para otros repositorios
    /// </summary>
    /// <remarks>No tiene Create</remarks>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T>
    where T : Identifiable
    {
        /// <summary>
        /// Obtiene un <see cref="T"/>
        /// </summary>
        /// <remarks>
        /// Puede ser nulo, si no existe
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        T? Read(string id);

        /// <summary>
        /// Obtiene todos los <see cref="T"/>
        /// </summary>
        /// <returns></returns>
        IMongoQueryable<T> ReadAll();

        /// <summary>
        /// Recupera todos los <see cref="T"/> de forma paginada.
        /// </summary>
        /// <typeparam name="T"></typeparam>.
        /// <param name="pageSize">El número máximo de objetos a recuperar por página.</param>
        /// <param name="pageNumber">El número de página a recuperar.</param>
        /// <returns></returns>
        public Task<IEnumerable<T>> ReadAllPaged(int pageSize, int pageNumber);

        /// <summary>
        /// Recupera todos los <see cref="T"/> de forma paginada.
        /// Los datos solo se cargan cuando hacen falta.
        /// </summary>
        /// <typeparam name="T"></typeparam>.
        /// <param name="pageSize">El número máximo de objetos a recuperar por página.</param>
        /// <returns></returns>
        public Task<IEnumerable<Lazy<IEnumerable<T>>>> ReadAllPagedLazy(int pageSize);


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
