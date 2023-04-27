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
    public class PostDAO : DAO<Post>
    {
        /// <summary>
        /// Construye un DAO para posts
        /// </summary>
        /// <param name="connectionString"></param>
        public PostDAO(string connectionString) : base(connectionString)
        {
        }

        protected override string GetCollectionName()
        {
            return "Posts";
        }
    }
}
