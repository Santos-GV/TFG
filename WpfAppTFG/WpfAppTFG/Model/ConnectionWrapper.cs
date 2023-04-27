namespace WpfAppTFG.Model
{
    using MongoDB.Driver;
    using SharpCompress;

    public sealed class ConnectionWrapper
    {
        // La conexión solo se crea en el momento que hace falta
        private static readonly Lazy<ConnectionWrapper> instance =
            new Lazy<ConnectionWrapper>(() => new ConnectionWrapper());
        // La conexión se abre y se cierra automaticamente por el driver
        private IMongoClient client { get; set; }
        private IMongoDatabase database { get; set; }
        public IMongoDatabase Database { get => database; }

        private ConnectionWrapper()
        {
        }

        public static ConnectionWrapper GetInstance(string connectionString)
        {
            // Si la conexión no existe (es null) se crea la conexión
            if (instance.Value.client == null)
            {
                instance.Value.client = new MongoClient(connectionString);
                instance.Value.database = instance.Value.client.GetDatabase("ForoSaber");
            }
            return instance.Value;
        }
    }
}
