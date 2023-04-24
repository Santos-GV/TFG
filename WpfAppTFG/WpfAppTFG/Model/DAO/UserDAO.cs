using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model.DAO
{
    public abstract class UserDAO : IDAO<User>
    {
        private readonly IMongoClient dbClient;
        private readonly IMongoDatabase db;

        /// <summary>
        /// Construye un DAO para usuarios
        /// </summary>
        /// <param name="connectionString"></param>
        public UserDAO(string connectionString)
        {
            this.dbClient = new MongoClient(connectionString);
            this.db = dbClient.GetDatabase("Usuarios");
        }

        /// <summary>
        /// Crea un usuario
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task Create(User user)
        {
            await GetCollection().InsertOneAsync(user);
        }

        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task Delete(User user)
        {
            await GetCollection().DeleteOneAsync(x => x.Id == user.Id);
        }

        /// <summary>
        /// Obtiene un usuario por su Id
        /// Devuelve null si no existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User?> Read(int id)
        {
            var user = await GetCollection().FindAsync(user => user.Id == id);
            return await user.SingleOrDefaultAsync();
        }

        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> ReadAll()
        {
            var usuarios = await GetCollection().FindAsync(x => true);
            return usuarios.ToEnumerable();
        }

        /// <summary>
        /// Actuliza un usuario
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task Update(User user)
        {
            await GetCollection().ReplaceOneAsync(x => x.Id == user.Id, user);
        }

        /// <summary>
        /// Obtiene la colección de usuarios
        /// </summary>
        /// <returns></returns>
        private IMongoCollection<User> GetCollection()
        {
            return db.GetCollection<User>("Usuarios");
        }
    }
}
