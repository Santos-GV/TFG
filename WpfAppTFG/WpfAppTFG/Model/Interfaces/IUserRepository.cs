using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfAppTFG.Model.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<Post?> GetPost(int id);
        Task<IEnumerable<Post>> GetAllPost();
        Task<Comentario?> GetComentario(int id);
        Task<IEnumerable<Comentario>> GetAllComentario();
    }
}
