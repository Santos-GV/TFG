using MongoDB.Driver.Linq;
using System.Linq;
using System.Threading.Tasks;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model.Respository
{
    /// <summary>
    /// Implementación de un Repositorio de <see cref="Post"/>
    /// </summary>
    public class Repository<T> : IRepository<T>
    where T : IIdentifiable
    {

        private readonly IDAO<T> entityDAO;

        public Repository(IDAO<T> dao)
        {
            this.entityDAO = dao;
        }

        /// <summary>
        /// Crea un <see cref="T"/>
        /// </summary>
        /// <param name="entiti"></param>
        /// <returns></returns>
        public virtual async Task Create(T entiti)
        {
            await entityDAO.Create(entiti);
        }

        /// <summary>
        /// Elimina un <see cref="T"/>
        /// </summary>
        /// <param name="entiti"></param>
        /// <returns></returns>
        public virtual async Task Delete(T entiti)
        {
            await entityDAO.Create(entiti);
        }

        /// <summary>
        /// Obtiene un <see cref="T"/> por su id
        /// </summary>
        /// <remarks>Puede ser nulo, si no existe</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T?> Read(int id)
        {
            var entity = await entityDAO.Read(id);
            return entity;
        }

        /// <summary>
        /// Obtiene todos los <see cref="T"/>
        /// </summary>
        /// <returns></returns>
        public IMongoQueryable<T> ReadAll()
        {
            var entities = entityDAO.ReadAll();
            return entities;
        }

        /// <summary>
        /// Actualiza un <see cref="T"/>
        /// </summary>
        /// <param name="entity></param>
        /// <returns></returns>
        public async Task Update(T entity)
        {
            await entityDAO.Update(entity);
        }
    }
}
