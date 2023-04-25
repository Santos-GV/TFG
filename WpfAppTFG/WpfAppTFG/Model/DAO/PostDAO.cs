using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model.DAO
{
    /// <summary>
    /// Implementación de un DAO de <see cref="Post"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PostDAO : IDAO<Post>
    {
        private readonly IMongoClient dbClient;
        private readonly IMongoDatabase db;

        /// <summary>
        /// Construye un DAO para posts
        /// </summary>
        /// <param name="connectionString"></param>
        public PostDAO(string connectionString)
        {
            this.dbClient = new MongoClient(connectionString);
            this.db = dbClient.GetDatabase("Posts");
        }

        /// <summary>
        /// Crea un post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task Create(Post post)
        {
            await GetCollection().InsertOneAsync(post);
        }

        /// <summary>
        /// Elimina un post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task Delete(Post post)
        {
            await GetCollection().DeleteOneAsync(x => x.Id == post.Id);
        }

        /// <summary>
        /// Obtiene un post por su Id
        /// Devuelve null si no existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Post?> Read(int id)
        {
            var post = await GetCollection().FindAsync(user => user.Id == id);
            return await post.SingleOrDefaultAsync();
        }

        /// <summary>
        /// Obtiene todos los post
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Post>> ReadAll()
        {
            var usuarios = await GetCollection().FindAsync(x => true);
            return usuarios.ToEnumerable();
        }

        /// <summary>
        /// Actuliza un post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task Update(Post post)
        {
            await GetCollection().ReplaceOneAsync(x => x.Id == post.Id, post);
        }

        /// <summary>
        /// Obtiene la colección de posts
        /// </summary>
        /// <returns></returns>
        private IMongoCollection<Post> GetCollection()
        {
            return db.GetCollection<Post>("Posts");
        }
    }
}
