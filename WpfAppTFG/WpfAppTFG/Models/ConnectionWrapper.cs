namespace WpfAppTFG.Model
{
    using MongoDB.Driver;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Resources;

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
                    var resourceDictionary = new ResourceDictionary();
                    resourceDictionary.Source = new Uri("pack://application:,,,/WpfAppTFG;component/ConnectionStringDictionary.xaml", UriKind.Absolute);
                    // Access a specific resource by key
                    var connectionString = resourceDictionary["connectionString"].ToString();
                    instance.Client = new MongoClient(connectionString);
                    instance.Database = instance.Client.GetDatabase("ForoSaber");
                }
                return instance;
            }
        }
    }
}
