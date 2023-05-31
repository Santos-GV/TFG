using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfAppTFG.Model.DAOs;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model.Respository
{
    /// <summary>
    /// Implementación de un IdRepositorio de <see cref="Comentario"/>
    /// </summary>
    public class ComentarioRepository : IIdRepository<Comentario>
    {
        private readonly IDAO<Post> postDAO;
        private readonly IDAO<Log> logDAO;
        private readonly User user;

        public ComentarioRepository(User user)
        {
            this.postDAO = new PostDAO();
            this.logDAO = new LogDAO();
            this.user = user;
        }

        /// <summary>
        /// Crea un <see cref="Comentario"/>
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="comentario"></param>
        /// <returns></returns>
        public async Task Create(string postId, Comentario comentario)
        {
            var post = postDAO.Read(postId);
            if (post is null) return;
            comentario.Id = ObjectId.GenerateNewId().ToString();
            post.Comentarios.Add(comentario);
            var postTask = postDAO.Update(post);
            var log = new Log(user.Id, $"Creado comentario `{comentario.Id}`");
            var logTask = logDAO.Create(log);
            await Task.WhenAll(postTask, logTask);

        }

        /// <summary>
        /// Elimina un <see cref="Comentario"/>
        /// </summary>
        /// <param name="comentario"></param>
        /// <returns></returns>
        public async Task Delete(Comentario comentario)
        {
            var posts = postDAO.ReadAll();
            var post = await posts
                .FirstOrDefaultAsync(post => post.Comentarios
                    .Any(_comentario => _comentario.Id == comentario.Id));
            if (post == null) return;
            post.Comentarios = post.Comentarios
                .Where(otherComentario => !otherComentario.Id.Equals(comentario.Id))
                .ToList();
            var postTask = postDAO.Update(post);
            var log = new Log(user.Id, $"Eliminado comentario `{comentario.Id}`");
            var logTask = logDAO.Create(log);
            await Task.WhenAll(postTask, logTask);

        }

        /// <summary>
        /// Obtiene todos los <see cref="Comentario"/>
        /// </summary>
        /// <returns></returns>
        public async Task<IMongoQueryable<Comentario>> ReadAll()
        {
            var posts = postDAO.ReadAll();
            var comentarios = posts.SelectMany(post => post.Comentarios);
            var log = new Log(user.Id, $"Lee todos los `{typeof(Comentario)}`");
            await logDAO.Create(log);
            return comentarios;
        }

        /// <summary>
        /// Obtiene un <see cref="Comentario"/> por su id
        /// </summary>
        /// <remarks>Puede ser nulo, si no existe</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Comentario?> Read(string id)
        {
            var posts = postDAO.ReadAll();
            var comentario = posts
                .SelectMany(post => post.Comentarios)
                .FirstOrDefaultAsync(comentario => comentario.Id == id);
            var log = new Log(user.Id, $"Lee comentario `{id}`");
            await logDAO.Create(log);
            return comentario.Result;
        }

        /// <summary>
        /// Recupera todos los <see cref="Comentario"/> de forma paginada.
        /// </summary>
        /// <param name="pageSize">El número máximo de objetos a recuperar por página.</param>
        /// <param name="pageNumber">El número de página a recuperar.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Comentario>> ReadAllPaged(int pageSize, int pageNumber)
        {
            // TODO: Make it paged
            var comentariosTask = postDAO.ReadAll()
                .SelectMany(post => post.Comentarios)
                .ToListAsync();
            var log = new Log(user.Id, $"Lee todos los `{typeof(Comentario)}` de forma paginada");
            var logTask = logDAO.Create(log);
            await Task.WhenAll(comentariosTask, logTask);
            return await comentariosTask;
        }

        /// <summary>
        /// Recupera todos los <see cref="Comentario"/> de forma paginada.
        /// Los datos solo se cargan cuando hacen falta.
        /// </summary>
        /// <param name="pageSize">El número máximo de objetos a recuperar por página.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Lazy<IEnumerable<Comentario>>>> ReadAllPagedLazy(int pageSize)
        {
            // TODO: Make it paged and lazy
            throw new NotImplementedException();
        }


        /// <summary>
        /// Actualiza un <see cref="Comentario"/>
        /// </summary>
        /// <param name="comentario"></param>
        /// <returns></returns>
        public async Task Update(Comentario comentario)
        {
            var posts = postDAO.ReadAll();
            var postTask = posts
                .FirstOrDefaultAsync(post => post.Comentarios
                    .Any(_comentario => _comentario.Id == comentario.Id));
            var log = new Log(user.Id, $"Actualiza comentario `{comentario.Id}`");
            var logTask = logDAO.Create(log);
            await Task.WhenAll(postTask, logTask);
            var post = await postTask;
            var oldComentario = post?.Comentarios
                .FirstOrDefault(_comentario => _comentario.Id == comentario.Id);
            if (oldComentario == null) return; // Comprueba que exista el comentario
            // TODO: Check code implementation
            oldComentario = comentario;
        }
    }
}
