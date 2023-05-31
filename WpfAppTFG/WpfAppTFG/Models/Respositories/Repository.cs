using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfAppTFG.Model.DAOs;
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
        private readonly IDAO<Log> logDAO;
        private readonly User? user;

        public Repository(IDAO<T> dao, User user)
        {
            this.entityDAO = dao;
            this.logDAO = new LogDAO();
            this.user = user;
        }

        /// <summary>
        /// Crea un <see cref="T"/>
        /// </summary>
        /// <param name="entiti"></param>
        /// <returns></returns>
        public virtual async Task Create(T entiti)
        {
            var entitiTask = entityDAO.Create(entiti);
            var log = new Log(user?.Id, $"Creado recurso `{entiti.Id}`");
            var logTask = logDAO.Create(log);
            await Task.WhenAll(entitiTask, logTask);
        }

        /// <summary>
        /// Elimina un <see cref="T"/>
        /// </summary>
        /// <param name="entiti"></param>
        /// <returns></returns>
        public virtual async Task Delete(T entiti)
        {
            var entitiTask = entityDAO.Delete(entiti);
            var log = new Log(user?.Id, $"Eliminado recurso `{entiti.Id}`");
            var logTask = logDAO.Create(log);
            await Task.WhenAll(entitiTask, logTask);
        }

        /// <summary>
        /// Obtiene un <see cref="T"/> por su id
        /// </summary>
        /// <remarks>Puede ser nulo, si no existe</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T?> Read(string id)
        {
            var entityTask = entityDAO.Read(id);
            var log = new Log(user?.Id, $"Lee recurso `{id}`");
            await logDAO.Create(log);
            return entityTask;
        }

        /// <summary>
        /// Obtiene todos los <see cref="T"/>
        /// </summary>
        /// <returns></returns>
        public async Task<IMongoQueryable<T>> ReadAll()
        {
            var entities = entityDAO.ReadAll();
            var log = new Log(user?.Id, $"Lee todos los `{typeof(T)}`");
            await logDAO.Create(log);
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
            var entitiesTask = entityDAO.ReadAllPaged(pageSize, pageNumber);
            var log = new Log(user?.Id, $"Lee todos los `{typeof(T)}` de forma paginada");
            var logTask = logDAO.Create(log);
            await Task.WhenAll(entitiesTask, logTask);
            return await entitiesTask;
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
            // TODO: Check log creating, it freezes
            //var log = new Log(user?.Id, $"Lee todos los `{typeof(T)}` de forma paginada y perezosa");
            //await logDAO.Create(log).Wait();
            return entities;
        }

        /// <summary>
        /// Actualiza un <see cref="T"/>
        /// </summary>
        /// <param name="entity></param>
        /// <returns></returns>
        public async Task Update(T entity)
        {
            var entitiTask = entityDAO.Update(entity);
            var log = new Log(user?.Id, $"Actualiza el recurso `{entity.Id}`");
            var logTask = logDAO.Create(log);
            await Task.WhenAll(entitiTask, logTask);
        }
    }
}
