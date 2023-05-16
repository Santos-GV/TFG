using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model.DAOs
{
    public abstract class DAO<T> : IDAO<T>
    where T : IIdentifiable
    {
        private readonly ConnectionWrapper connection;

        /// <summary>
        /// Construye un DAO de <see cref="T"/>
        /// </summary>
        /// <param name="connectionString"></param>
        public DAO()
        {
            this.connection = ConnectionWrapper.Instance;
        }

        /// <summary>
        /// Crea un <see cref="T"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Create(T entity)
        {
            await GetCollection().InsertOneAsync(entity);
        }

        /// <summary>
        /// Elimina un <see cref="T"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Delete(T entity)
        {
            await GetCollection().DeleteOneAsync(otherEntiry => otherEntiry.GetId() == entity.GetId());
        }

        /// <summary>
        /// Obtiene un <see cref="T"/> por su Id
        /// Devuelve null si no existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T?> Read(string id)
        {
            var entity = await Find(entity => entity.GetId().Equals(id)).FirstOrDefaultAsync();
            return entity;
        }

        /// <summary>
        /// Obtiene todos los <see cref="T"/>
        /// </summary>
        /// <returns></returns>
        public IMongoQueryable<T> ReadAll()
        {
            var entities = Find(_ => true);
            return entities;
        }

        /// <summary>
        /// Recupera todos los <see cref="T"/> de forma paginada.
        /// </summary>
        /// <typeparam name="T"></typeparam>.
        /// <param name="pageSize">El número máximo de objetos a recuperar por página.</param>
        /// <param name="pageNumber">El número de página a recuperar.</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ReadAllPaged(int pageSize, int pageNumber = 1)
        {
            // Determina el número de documentos que deben omitirse
            // en función del tamaño y el número de página
            // Empieza sobre 0 asique se resta 1
            int skipAmount = pageSize * (pageNumber - 1);
            var retrievedData = await GetCollection()
                .Find(_ => true)
                .Skip(skipAmount)
                .ToListAsync();
            return retrievedData;
        }

        /// <summary>
        /// Recupera todos los <see cref="T"/> de forma paginada.
        /// Los datos solo se cargan cuando hacen falta.
        /// </summary>
        /// <typeparam name="T"></typeparam>.
        /// <param name="pageSize">El número máximo de objetos a recuperar por página.</param>
        /// <returns>Un <see cref="IEnumerable"/> de paginas con un <see cref="IEnumerable"/> de los elementos de esa página</returns>
        public async Task<IEnumerable<Lazy<IEnumerable<T>>>> ReadAllPagedLazy(int pageSize)
        {
            var total = Find(_ => true).Count(); // CountAsync doesn't work
            var numPages = (int)Math.Ceiling((double)total / pageSize);
            var pages = Enumerable.Range(0, numPages)
                .Select(i =>
                {
                    var startIndex = i * pageSize;
                    // En caso de tener menos elementos que el tamaÃ±o de la pagina
                    // devulven los elementos que queden
                    var endIndex = Math.Min(startIndex + pageSize, total);
                    var items = GetCollection().Find(_ => true)
                        .Skip(startIndex)
                        .Limit(endIndex - startIndex)
                        .ToEnumerable();
                    return new Lazy<IEnumerable<T>>(() => items);
                });
            return pages;
            // TODO: Find a way to chunk it with a IMongoQueriable, it dosent support Chunk operation
            /*
            var entites = Find(_ => true).AsEnumerable()
                .Chunk(pageSize)
                .Select(x => new Lazy<IEnumerable<T>>(() => x.AsEnumerable()));
            return entites;
            */
        }


        /// <summary>
        /// Actuliza un <see cref="T"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Update(T entity)
        {
            await GetCollection().ReplaceOneAsync(otherEntity => otherEntity.GetId() == entity.GetId(), entity);
        }

        /// <summary>
        /// Busca un <see cref="T"/> que cumpla la condición del argumento
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private IMongoQueryable<T> Find(Expression<Func<T, bool>> condition)
        {
            var entities = GetCollection().AsQueryable().Where(condition);
            return entities;
        }


        /// <summary>
        /// Obtiene una colección de <see cref="T"/>
        /// </summary>
        /// <returns></returns>
        private IMongoCollection<T> GetCollection()
        {
            return connection.Database.GetCollection<T>(GetCollectionName());
        }

        /// <summary>
        /// Obtiene el nombre de la colección de <see cref="T"/>
        /// </summary>
        /// <returns></returns>
        protected abstract string GetCollectionName();
    }
}
