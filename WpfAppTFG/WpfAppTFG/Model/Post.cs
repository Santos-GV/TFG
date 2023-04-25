using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace WpfAppTFG.Model
{
    /// <summary>
    /// Representa un post
    /// </summary>
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
        [BsonElement("id_usuario")]
        public int IdUsuario { get; set; }
        [BsonElement("titulo")]
        public string Titulo { get; set; }
        [BsonElement("contenido")]
        public string Contenido { get; set; }
        [BsonElement("etiquetas")]
        public List<string> Etiquetas { get; set; }
        [BsonElement("Comentarios")]
        public List<Comentario> Comentarios { get; set; }

        public Post(int idUsuario, string titulo, string contenido, List<string> etiquetas)
        {
            IdUsuario = idUsuario;
            Titulo = titulo;
            Contenido = contenido;
            Etiquetas = etiquetas;
            Comentarios = new List<Comentario>();
        }
    }
}