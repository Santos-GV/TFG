using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model.Respository
{
    /// <summary>
    /// Implementación de un Repositorio de <see cref="User"/>
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly IDAO<User> userDAO;
        private readonly IDAO<Comentario> comentarioDAO;
        private readonly IDAO<Post> postDAO;

        public UserRepository(IDAO<User> userDAO, IDAO<Comentario> comentarioDAO, IDAO<Post> postDAO)
        {
            this.userDAO = userDAO;
            this.comentarioDAO = comentarioDAO;
            this.postDAO = postDAO;
        }

        /// <summary>
        /// Crea un objeto un usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task Create(User user)
        {
            await userDAO.Create(user);
        }

        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task Delete(User user)
        {
            await userDAO.Delete(user);
            var posts =  await postDAO.ReadAll();
            posts
                .Where(post => post.IdUsuario == user.Id)
                .ToList()
                .ForEach(post => postDAO.Delete(post));
            var comentarios = await comentarioDAO.ReadAll();
            comentarios
                .Where(comentario => comentario.IdUsuario == user.Id)
                .ToList()
                .ForEach(comentario => comentarioDAO.Delete(comentario));
        }

        /// <summary>
        /// Obtiene todos los comentarios de un usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Comentario>> GetAllComentario(int userId)
        {
            var usuario = await userDAO.Read(userId);
            var comentarios = await comentarioDAO.ReadAll();
            return comentarios.Where(x => x.IdUsuario == usuario?.Id);
        }

        /// <summary>
        /// Obtiene todos los posts de un usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Post>> GetAllPost(int userId)
        {
            var usuario = await userDAO.Read(userId);
            var posts = await postDAO.ReadAll();
            return posts.Where(x => x.IdUsuario == usuario?.Id);
        }

        /// <summary>
        /// Obtiene un usuario
        /// Puede ser nulo, si no existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User?> Read(int id)
        {
            User? usuario = await userDAO.Read(id);
            return usuario;
        }

        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> ReadAll()
        {
            IEnumerable<User> usuarios = await userDAO.ReadAll();
            return usuarios;
        }

        /// <summary>
        /// Actualiza un usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task Update(User user)
        {
            await userDAO.Update(user);
        }
    }
}
