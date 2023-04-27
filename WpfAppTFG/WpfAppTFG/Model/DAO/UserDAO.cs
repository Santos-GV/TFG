using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model.DAO
{
    /// <summary>
    /// Implementación de un DAO de <see cref="User"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UserDAO : DAO<User>
    {
        /// <summary>
        /// Construye un DAO para usuarios
        /// </summary>
        /// <param name="connectionString"></param>
        public UserDAO(string connectionString) : base(connectionString)
        {
        }

        protected override string GetCollectionName()
        {
            return "Usuarios";
        }
    }
}
