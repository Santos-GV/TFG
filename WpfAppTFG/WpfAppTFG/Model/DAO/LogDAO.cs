using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model.DAO
{
    /// <summary>
    /// Implementación de un DAO de <see cref="Log"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LogDAO : IDAO<Log>
    {
        private readonly IMongoClient dbClient;
        private readonly IMongoDatabase db;

        /// <summary>
        /// Construye un DAO para logs
        /// </summary>
        /// <param name="connectionString"></param>
        public LogDAO(string connectionString)
        {
            this.dbClient = new MongoClient(connectionString);
            this.db = dbClient.GetDatabase("Logs");
        }

        /// <summary>
        /// Crea un log
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public async Task Create(Log log)
        {
            await GetCollection().InsertOneAsync(log);
        }

        /// <summary>
        /// Elimina un log
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public async Task Delete(Log log)
        {
            await GetCollection().DeleteOneAsync(x => x.Id == log.Id);
        }

        /// <summary>
        /// Obtiene un log por su Id
        /// Devuelve null si no existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Log?> Read(int id)
        {
            var log = await GetCollection().FindAsync(user => user.Id == id);
            return await log.SingleOrDefaultAsync();
        }

        /// <summary>
        /// Obtiene todos los logs
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Log>> ReadAll()
        {
            var usuarios = await GetCollection().FindAsync(x => true);
            return usuarios.ToEnumerable();
        }

        /// <summary>
        /// Actuliza un log
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>

        public async Task Update(Log log)
        {
            await GetCollection().ReplaceOneAsync(x => x.Id == log.Id, log);
        }

        /// <summary>
        /// Obtiene la colección de logs
        /// </summary>
        /// <returns></returns>
        private IMongoCollection<Log> GetCollection()
        {
            return db.GetCollection<Log>("Logs");
        }
    }
}
