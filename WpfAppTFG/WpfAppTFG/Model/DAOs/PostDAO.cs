namespace WpfAppTFG.Model.DAOs
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
        public PostDAO() : base()
        {
        }

        protected override string GetCollectionName()
        {
            return "Posts";
        }
    }
}
