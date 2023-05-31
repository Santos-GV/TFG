using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using WpfAppTFG.Model;
using WpfAppTFG.Model.Respository;

namespace WpfAppTFG.Controller
{
    public class LoginController
    {
        private readonly UserRepository userRepository;

        public LoginController()
        {
            userRepository = new UserRepository();
        }

        /// <summary>
        /// Inicia sesión con un usuario y su contraseña
        /// Puede devolver null, si el usuario no existe
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPsswd"></param>
        /// <returns>Si el login se produjo con exito</returns>
        public async Task<User?> Login(string userName, string userPsswd)
        {
            var user = await userRepository.ReadAll()
                .Where(user => user.Name.Equals(userName))
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (user == null) return null;
            var isPsswdCorrect = user.CheckPsswd(userPsswd);
            if (!isPsswdCorrect) return null;
            return user;
        }
    }
}
