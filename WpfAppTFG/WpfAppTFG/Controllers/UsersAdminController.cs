using MongoDB.Driver.Linq;
using System.Linq;
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
        public User? SearchUser(string userName)
        {
            var user = userRepository
                .ReadAll()
                .FirstOrDefault(x => x.Name.Equals(userName));
            return user;
        }

        public async Task Delete(User user)
        {
            await userRepository.Delete(user);
        }
    }
}
