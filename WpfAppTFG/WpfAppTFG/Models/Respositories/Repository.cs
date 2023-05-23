using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model.Respository
{
    /// <summary>
    /// Implementación de un Repositorio de <see cref="Post"/>
    /// </summary>
    public class Repository<T> : IRepository<T>
    where T : Identifiable
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
        public async Task<T?> Read(string id)
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
        /// Recupera todos los <see cref="T"/> de forma paginada.
        /// </summary>
        /// <typeparam name="T"></typeparam>.
        /// <param name="pageSize">El número máximo de objetos a recuperar por página.</param>
        /// <param name="pageNumber">El número de página a recuperar.</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ReadAllPaged(int pageSize, int pageNumber)
        {
            var entities = await entityDAO.ReadAllPaged(pageSize, pageNumber);
            return entities;
        }

        /// <summary>
        /// Recupera todos los <see cref="T"/> de forma paginada.
        /// Los datos solo se cargan cuando hacen falta.
        /// </summary>
        /// <typeparam name="T"></typeparam>.
        /// <param name="pageSize">El número máximo de objetos a recuperar por página.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Lazy<IEnumerable<T>>>> ReadAllPagedLazy(int pageSize)
        {
            var entities = await entityDAO.ReadAllPagedLazy(pageSize);
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
