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
