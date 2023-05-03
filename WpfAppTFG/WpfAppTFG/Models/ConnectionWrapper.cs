namespace WpfAppTFG.Model
{
    using MongoDB.Driver;

    /// <summary>
    /// Encapsulador de la conexión a la base de datos.
    /// </summary>
    public sealed class ConnectionWrapper
    {
        // La conexión se abre y se cierra automaticamente por el driver
        private static readonly ConnectionWrapper instance = new ConnectionWrapper();
        private IMongoClient Client { get; set; }
        public IMongoDatabase Database { get; private set; }

        private ConnectionWrapper()
        {
        }

        public static ConnectionWrapper Instance
        {
            get
            {
                // Si la conexión no existe (es null) se crea la conexión
                if (instance.Client == null)
                {
                    instance.Client = new MongoClient("mongodb://localhost:27017");
                    instance.Database = instance.Client.GetDatabase("ForoSaber");
                }
                return instance;
            }
        }
    }
}
