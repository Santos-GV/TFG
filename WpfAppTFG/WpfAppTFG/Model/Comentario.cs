using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model
{
    /// <summary>
    /// Representa un comentario
    /// Se encuentra dentro de los post, no tiene sentido por si mismo
    /// </summary>
    public class Comentario : IIdentifiable
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

        /// <summary>
        /// Obtiene el Id del <see cref="Post"/>
        /// </summary>
        /// <returns></returns>
        public int GetId()
        {
            return this.Id;
        }
    }
}