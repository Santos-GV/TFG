namespace WpfAppTFG.Model.DAOs
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
        public UserDAO() : base()
        {
        }

        protected override string GetCollectionName()
        {
            return "Users";
        }
    }
}
