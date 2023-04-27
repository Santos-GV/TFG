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
