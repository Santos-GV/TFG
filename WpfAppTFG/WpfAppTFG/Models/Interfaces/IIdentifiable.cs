using MongoDB.Bson.Serialization.Attributes;

namespace WpfAppTFG.Model.Interfaces
{
    /// <summary>
    /// Representa un objeto que se puede indentificar de forma única
    /// </summary>
    public abstract class Identifiable
    {
        [BsonIgnore]
        public abstract string Id { get; set; }
    }
}
