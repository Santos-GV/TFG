using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WpfAppTFG.Model
{
    public enum Rol : byte
    {
        [BsonRepresentation(BsonType.String)]
        Regular,
        [BsonRepresentation(BsonType.String)]
        Moderador,
        [BsonRepresentation(BsonType.String)]
        Administrador
    }
}