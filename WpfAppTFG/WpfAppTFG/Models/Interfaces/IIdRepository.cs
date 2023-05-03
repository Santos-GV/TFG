using System.Threading.Tasks;

namespace WpfAppTFG.Model.Interfaces
{
    /// <summary>
    /// Representación abstracta de un IdRepositorio genérico
    /// </summary>
    /// <remarks>
    /// Se diferencia en un Repository en que para poder crear un objeto del tipo del
    /// Repository hay que dar el id de otra entidad
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public interface IIdRepository<T> : IBaseRepository<T>
    where T : IIdentifiable
    {
        /// <summary>
        /// Crea un <see cref="T"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Create(string id, T entity);
    }
}
