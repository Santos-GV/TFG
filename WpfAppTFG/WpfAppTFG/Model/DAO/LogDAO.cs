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
    public class LogDAO : DAO<Log>
    {
        /// <summary>
        /// Construye un DAO para logs
        /// </summary>
        /// <param name="connectionString"></param>
        public LogDAO(string connectionString) : base(connectionString)
        {
        }

        protected override string GetCollectionName()
        {
            return "Logs";
        }
    }
}
