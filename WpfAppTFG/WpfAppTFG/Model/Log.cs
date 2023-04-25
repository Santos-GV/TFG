using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WpfAppTFG.Model
{
    /// <summary>
    /// Representa un log
    /// </summary>
    public class Log
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
        [BsonElement("id_usuario")]
        public int IdUsuario { get; set; }
        [BsonElement("accion")]
        public string Accion { get; set; }

        public Log(int idUsuario, string accion)
        {
            IdUsuario = idUsuario;
            Accion = accion;
        }
    }
}
