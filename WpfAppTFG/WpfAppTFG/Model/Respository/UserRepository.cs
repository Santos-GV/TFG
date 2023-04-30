using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Threading.Tasks;
using WpfAppTFG.Model.DAO;
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

        public UserRepository() : base(new UserDAO())
        {
            this.userDAO = new UserDAO();
            this.postDAO = new PostDAO();
            this.logDAO = new LogDAO();
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
                .Where(otherUser => otherUser.Name == user.Name)
                .FirstOrDefault();
            if (otherUser == null)
            {
                throw new UserAlreadyExists($"El nombre {user.Name} ya está en uso");
            }
            await userDAO.Create(user);
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
            // Espera asíncrona y no bloqueantemente a que se eliminen todos los datos
            await Task.WhenAll(postsTask, comentariosTask);
        }

        /// <summary>
        /// Obtiene todos los comentarios de un <see cref="User"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IQueryable<Comentario> ReadAllComentario(string userId)
        {
            var comentarios = postDAO.ReadAll().SelectMany(post => post.Comentarios);
            var comentariosUsuario = comentarios.Where(x => x.IdUsuario.Equals(userId));
            return comentariosUsuario;
        }

        /// <summary>
        /// Obtiene todos los logs de un <see cref="User"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IQueryable<Log> ReadAllLogs(string userId)
        {
            var logs = logDAO.ReadAll();
            var logsUsuario = logs.Where(x => x.IdUsuario.Equals(userId));
            return logsUsuario;
        }

        /// <summary>
        /// Obtiene todos los posts de un <see cref="User"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IQueryable<Post> ReadAllPost(string userId)
        {
            var posts = postDAO.ReadAll();
            var postUsuario = posts.Where(x => x.IdUsuario.Equals(userId));
            return postUsuario;
        }
    }
}
