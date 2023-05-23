using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfAppTFG.Model.Interfaces
{
    /// <summary>
    /// Representación abstracta de un Repositorio genérico
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> : IBaseRepository<T>
    where T : Identifiable
    {
        /// <summary>
        /// Crea un <see cref="T"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Create(T entity);
    }
}
