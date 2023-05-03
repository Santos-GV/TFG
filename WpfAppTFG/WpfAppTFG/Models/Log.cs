using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model
{
    /// <summary>
    /// Representa un log
    /// </summary>
    public class Log : IIdentifiable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("id_usuario")]
        public string IdUsuario { get; set; }
        [BsonElement("accion")]
        public string Accion { get; set; }

        public Log(string idUsuario, string accion)
        {
            IdUsuario = idUsuario;
            Accion = accion;
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
