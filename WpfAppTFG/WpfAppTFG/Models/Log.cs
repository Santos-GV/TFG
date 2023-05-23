using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model
{
    /// <summary>
    /// Representa un log
    /// </summary>
    public class Log : Identifiable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public override string Id { get; set; }
        [BsonElement("id_usuario")]
        public string IdUsuario { get; set; }
        [BsonElement("accion")]
        public string Accion { get; set; }

        public Log(string idUsuario, string accion)
        {
            IdUsuario = idUsuario;
            Accion = accion;
        }
    }
}
