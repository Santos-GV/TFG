namespace WpfAppTFG.Model
{
    using Amazon.SecurityToken.Model;
    using MongoDB.Driver;
    using SharpCompress;

    /// <summary>
    /// Encapsulador de la conexión a la base de datos.
    /// </summary>
    public sealed class ConnectionWrapper
    {
        // La conexión se abre y se cierra automaticamente por el driver
        private static readonly ConnectionWrapper instance = new ConnectionWrapper();
        private IMongoClient client { get; set; }
        // La conexión a los datos solo se crea en el momento que hace falta
        private Lazy<IMongoDatabase> database { get; set; }
        public IMongoDatabase Database { get => database.Value; }

        private ConnectionWrapper()
        {
        }

        public static ConnectionWrapper Instance
        {
            get
            {
                // Si la conexión no existe (es null) se crea la conexión
                if (instance.client == null)
                {
                    instance.client = new MongoClient("TODO: connectionString");
                    instance.database = new Lazy<IMongoDatabase>(() =>  instance.client.GetDatabase("ForoSaber"));
                }
                return instance;
            }
        }
    }
}
