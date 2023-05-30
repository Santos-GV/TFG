using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model
{
    /// <summary>
    /// Representa un comentario
    /// Se encuentra dentro de los post, no tiene sentido por si mismo
    /// </summary>
    public class Comentario : Identifiable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id_comentario")]
        public override string Id { get; set; }
        [BsonElement("id_usuario")]
        public string IdUsuario { get; set; }
        [BsonElement("contenido")]
        public string Contenido { get; set; }

        public Comentario(string idUsuario, string contenido)
        {
            IdUsuario = idUsuario;
            Contenido = contenido;
        }
    }
}