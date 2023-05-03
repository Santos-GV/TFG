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
        public string Id { get; set; }
        [BsonElement("id_usuario")]
        public string IdUsuario { get; set; }
        [BsonElement("contenido")]
        public string Contenido { get; set; }

        public Comentario(string idUsuario, string contenido)
        {
            IdUsuario = idUsuario;
            Contenido = contenido;
        }

        /// <summary>
        /// Obtiene el Id del <see cref="Post"/>
        /// </summary>
        /// <returns></returns>
        public string GetId()
        {
            return this.Id;
        }
    }
}