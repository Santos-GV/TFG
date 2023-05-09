﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model
{
    /// <summary>
    /// Representa un post
    /// </summary>
    public class Post : IIdentifiable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("id_usuario")]
        public string IdUsuario { get; set; }
        [BsonElement("titulo")]
        public string Titulo { get; set; }
        [BsonElement("contenido")]
        public string Contenido { get; set; }
        [BsonElement("etiquetas")]
        public List<string> Etiquetas { get; set; }
        [BsonElement("Comentarios")]
        public List<Comentario> Comentarios { get; set; }

        public Post(string idUsuario, string titulo, string contenido, List<string> etiquetas)
        {
            IdUsuario = idUsuario;
            Titulo = titulo;
            Contenido = contenido;
            Etiquetas = etiquetas;
            Comentarios = new List<Comentario>();
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