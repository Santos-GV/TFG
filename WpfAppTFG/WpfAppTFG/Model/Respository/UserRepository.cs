using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model.Respository
{
    /// <summary>
    /// Implementación de un Repositorio de <see cref="User"/>
    /// </summary>z
    public class UserRepository : IUserRepository
    {
        private readonly IDAO<User> userDAO;
        private readonly IDAO<Comentario> comentarioDAO;
        private readonly IDAO<Post> postDAO;
        private readonly IDAO<Log> logDAO;

        public UserRepository(IDAO<User> userDAO, IDAO<Comentario> comentarioDAO, IDAO<Post> postDAO, IDAO<Log> logDAO)
        {
            this.userDAO = userDAO;
            this.comentarioDAO = comentarioDAO;
            this.postDAO = postDAO;
            this.logDAO = logDAO;
        }

        /// <summary>
        /// Crea un objeto un <see cref="User"/>
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task Create(User user)
        {
            await userDAO.Create(user);
        }

        /// <summary>
        /// Elimina un <see cref="User"/>
        /// </summary>
        /// <remarks>Tambien elimina todos sus <see cref="Comentario"/> y <see cref="Post"/></remarks>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task Delete(User user)
        {
            await userDAO.Delete(user);
            // Obtiene los posts y comentarios del usuario de forma concurrente
            var postsTask =  postDAO.ReadAll();
            var comentariosTask = comentarioDAO.ReadAll();
            await Task.WhenAll(postsTask, comentariosTask);
            var posts = await postsTask;
            var comentarios = await comentariosTask;
            // Elimina los post y comentarios del usuario de forma concurrente
            var tasks = new List<Task>();
            tasks.AddRange(posts
                .AsParallel()
                .Where(post => post.IdUsuario == user.Id)
                .Select(post => postDAO.Delete(post)));
            tasks.AddRange(comentarios
                .AsParallel()
                .Where(comentario => comentario.IdUsuario == user.Id)
                .Select(comentario => comentarioDAO.Delete(comentario)));
            // Espera asíncrona y no bloqueantemente a que se eliminen todos los datos
            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Obtiene todos los comentarios de un <see cref="User"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Comentario>> GetAllComentario(int userId)
        {
            var comentarios = await comentarioDAO.ReadAll();
            var comentariosUsuario = comentarios.AsParallel().Where(x => x.IdUsuario == userId);
            return comentariosUsuario;
        }

        /// <summary>
        /// Obtiene todos los logs de un <see cref="User"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Log>> GetAllLogs(int userId)
        {
            var logs = await logDAO.ReadAll();
            var logsUsuario = logs.AsParallel().Where(x => x.IdUsuario == userId);
            return logsUsuario;
        }

        /// <summary>
        /// Obtiene todos los posts de un <see cref="User"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Post>> GetAllPost(int userId)
        {
            var posts = await postDAO.ReadAll();
            var postUsuario = posts.AsParallel().Where(x => x.IdUsuario == userId);
            return postUsuario;
        }

        /// <summary>
        /// Obtiene un <see cref="User"/>
        /// </summary>
        /// <remarks>Puede ser nulo, si no existe</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User?> Read(int id)
        {
            var usuario = await userDAO.Read(id);
            return usuario;
        }

        /// <summary>
        /// Obtiene todos los <see cref="User"/>
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> ReadAll()
        {
            var usuarios = await userDAO.ReadAll();
            return usuarios;
        }

        /// <summary>
        /// Actualiza un <see cref="User"/>
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task Update(User user)
        {
            await userDAO.Update(user);
        }
    }
}
