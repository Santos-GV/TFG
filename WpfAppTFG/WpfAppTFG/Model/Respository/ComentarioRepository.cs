﻿using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfAppTFG.Model.DAO;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model.Respository
{
    /// <summary>
    /// Implementación de un IdRepositorio de <see cref="Comentario"/>
    /// </summary>
    public class ComentarioRepository : IIdRepository<Comentario>
    {
        private readonly IDAO<Post> postDAO;

        public ComentarioRepository()
        {
            this.postDAO = new PostDAO();
        }

        /// <summary>
        /// Crea un <see cref="Comentario"/>
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="comentario"></param>
        /// <returns></returns>
        public async Task Create(int postId, Comentario comentario)
        {
            var post = await postDAO.Read(postId);
            post?.Comentarios.Add(comentario);
        }

        /// <summary>
        /// Elimina un <see cref="Comentario"/>
        /// </summary>
        /// <param name="comentario"></param>
        /// <returns></returns>
        public async Task Delete(Comentario comentario)
        {
            var posts = await postDAO.ReadAll();
            var post = posts
                .AsParallel()
                .FirstOrDefault(post => post.Comentarios
                    .Any(_comentario => _comentario.Id == comentario.Id));
            post?.Comentarios.Remove(comentario);
        }

        /// <summary>
        /// Obtiene todos los <see cref="Comentario"/>
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Comentario>> ReadAll()
        {
            var posts = await postDAO.ReadAll();
            var comentarios = posts.AsParallel().SelectMany(post => post.Comentarios);
            return comentarios;
        }

        /// <summary>
        /// Obtiene un <see cref="Comentario"/> por su id
        /// </summary>
        /// <remarks>Puede ser nulo, si no existe</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Comentario?> Read(int id)
        {
            var posts = await postDAO.ReadAll();
            var comentario = posts
                .AsParallel()
                .SelectMany(post => post.Comentarios)
                .FirstOrDefault(comentario => comentario.Id == id);
            return comentario;
        }

        /// <summary>
        /// Actualiza un <see cref="Comentario"/>
        /// </summary>
        /// <param name="comentario"></param>
        /// <returns></returns>
        public async Task Update(Comentario comentario)
        {
            var posts = await postDAO.ReadAll();
            var post = posts
                .AsParallel()
                .FirstOrDefault(post => post.Comentarios
                    .Any(_comentario => _comentario.Id == comentario.Id));
            var oldComentario = post?.Comentarios
                .AsParallel()
                .FirstOrDefault(_comentario => _comentario.Id == comentario.Id);
            if (oldComentario == null) return; // Comprueba que exista el comentario
            // TODO: Check code implementation
            oldComentario = comentario;
        }
    }
}
