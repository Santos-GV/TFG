using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfAppTFG.Model.Interfaces
{
    /// <summary>
    /// Representación abstracta de un DAO genérico
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDAO<T>
    {
        /// <summary>
        /// Crea un objeto del tipo del DAO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Create(T entity);

        /// <summary>
        /// Obtiene un objeto del tipo del DAO
        /// Puede ser nulo, si no existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T?> Read(int id);

        /// <summary>
        /// Obtiene todos los objetos del tipo del DAO
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> ReadAll();

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
        /// Actualiza un objeto del tipo del DAO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Update(T entity);

        /// <summary>
        /// Elimina un objeto del tipo del DAO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Delete(T entity);
    }
}
