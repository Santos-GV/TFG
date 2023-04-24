using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WpfAppTFG.Model
{
    public class Comentario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
        [BsonElement("id_usuario")]
        public int IdUsuario { get; set; }
        [BsonElement("contenido")]
        public string Contenido { get; set; }

        public Comentario(int idUsuario, string contenido)
        {
            IdUsuario = idUsuario;
            Contenido = contenido;
        }
    }
}