using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using WpfAppTFG.Model;
using WpfAppTFG.Model.Respository;

namespace WpfAppTFG.Controllers
{
    public class UsersAdminController
    {
        private readonly UserRepository userRepository;

        public UsersAdminController()
        {
            userRepository = new UserRepository();
        }

        /// <summary>
        /// Busca un usuario por su nombre.
        /// Puede devolver null, si el nombre de usuario no existe
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<User?> SearchUser(string userName)
        {
            var user = await userRepository
                .ReadAll()
                .FirstOrDefaultAsync(x => x.Name.Equals(userName));
            return user;
        }

        public async Task Delete(User user)
        {
            await userRepository.Delete(user);
        }
    }
}
