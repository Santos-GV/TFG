using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Threading.Tasks;
using WpfAppTFG.Model.DAOs;
using WpfAppTFG.Model.Exception;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model.Respository
{
    /// <summary>
    /// Implementación de un Repositorio de <see cref="User"/>
    /// </summary>z
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IDAO<User> userDAO;
        private readonly IDAO<Post> postDAO;
        private readonly IDAO<Log> logDAO;
        private readonly User user;

        public UserRepository() : base(new UserDAO(), null)
        {
            this.userDAO = new UserDAO();
            this.postDAO = new PostDAO();
            this.logDAO = new LogDAO();
            this.user = null;
        }

        /// <summary>
        /// Crea un objeto un <see cref="User"/>
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="UserAlreadyExists"></exception>
        public override async Task Create(User user)
        {
            var usuarios = userDAO.ReadAll();
            var otherUser = usuarios
                .Where(otherUser => otherUser.Name.Equals(user.Name))
                .FirstOrDefault();
            if (otherUser != null)
            {
                throw new UserAlreadyExists($"El nombre {user.Name} ya está en uso");
            }
            // TODO: Check create for user and log, both get freeze
            await userDAO.Create(user);
            //var log = new Log(user.Id, $"Creado usuario `{user.Id}`");
            //await logDAO.Create(log);
        }

        /// <summary>
        /// Elimina un <see cref="User"/>
        /// </summary>
        /// <remarks>Tambien elimina todos sus <see cref="Comentario"/> y <see cref="Post"/></remarks>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public override async Task Delete(User user)
        {
            await userDAO.Delete(user);
            // Elimina los post y comentarios del usuario de forma concurrente
            var postsTask = postDAO.ReadAll()
                .Where(post => post.IdUsuario == user.Id)
                .ForEachAsync(post => postDAO.Delete(post));
            var comentariosTask = postDAO.ReadAll()
                .ForEachAsync(post =>
                {
                    if (post.Comentarios.Select(c => c.IdUsuario).Contains(user.Id))
                    {
                        post.Comentarios.RemoveAll(comentario => comentario.IdUsuario.Equals(user.Id));
                        postDAO.Update(post);
                    }
                });
            var log = new Log(user.Id, $"Eliminado usuario `{user.Id}`");
            var logTask = logDAO.Create(log);
            // Espera asíncrona y no bloqueantemente a que se eliminen todos los datos
            await Task.WhenAll(postsTask, comentariosTask, logTask);
        }

        /// <summary>
        /// Obtiene todos los comentarios de un <see cref="User"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IQueryable<Comentario>> ReadAllComentario(string userId)
        {
            var comentarios = postDAO.ReadAll().SelectMany(post => post.Comentarios);
            var comentariosUsuario = comentarios.Where(x => x.IdUsuario.Equals(userId));
            var log = new Log(user.Id, $"Lee todos los `{typeof(Comentario)}` del usuario `{userId}`");
            await logDAO.Create(log);
            return comentariosUsuario;
        }

        /// <summary>
        /// Obtiene todos los logs de un <see cref="User"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IQueryable<Log>> ReadAllLogs(string userId)
        {
            var logs = logDAO.ReadAll();
            var logsUsuario = logs.Where(x => x.IdUsuario.Equals(userId));
            var log = new Log(user.Id, $"Lee todos los `{typeof(Log)}` de `{userId}`");
            await logDAO.Create(log);
            return logsUsuario;
        }

        /// <summary>
        /// Obtiene todos los posts de un <see cref="User"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IQueryable<Post>> ReadAllPost(string userId)
        {
            var posts = postDAO.ReadAll();
            var postUsuario = posts.Where(x => x.IdUsuario.Equals(userId));
            var log = new Log(user.Id, $"Lee todos los ``{typeof(Post)}` `{userId}`");
            await logDAO.Create(log);
            return postUsuario;
        }
    }
}
