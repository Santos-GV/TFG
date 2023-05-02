namespace WpfAppTFG.Model.DAOs
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
        public LogDAO() : base()
        {
        }

        protected override string GetCollectionName()
        {
            return "Logs";
        }
    }
}
