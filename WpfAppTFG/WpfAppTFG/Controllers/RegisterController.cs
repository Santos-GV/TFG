using System.Threading.Tasks;
using WpfAppTFG.Model;
using WpfAppTFG.Model.Exception;
using WpfAppTFG.Model.Respository;

namespace WpfAppTFG.Controller
{
    public class RegisterController
    {
        private readonly UserRepository userRepository;

        public RegisterController()
        {
            userRepository = new UserRepository();
        }

        /// <summary>
        /// Registra un usuario en la aplicación
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPsswd"></param>
        /// <returns>Si la se ha añadido</returns>
        public async Task<bool> Register(string userName, string userPsswd)
        {
            var user = new User(userName, userPsswd);
            try
            {
                await userRepository.Create(user);
                return true;
            }
            catch (UserAlreadyExists)
            {
                return false;
            }
        }
    }
}
