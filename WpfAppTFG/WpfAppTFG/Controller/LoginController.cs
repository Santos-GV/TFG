using System.Threading.Tasks;
using WpfAppTFG.Model;
using WpfAppTFG.Model.Exception;
using WpfAppTFG.Model.Respository;

namespace WpfAppTFG.Controller
{
    public class LoginController
    {
        private readonly UserRepository userRepository;

        public LoginController()
        {
            // TODO: Add comentarioDAO
            userRepository = new UserRepository();
        }

        public async Task<bool> Login(string userName, string userPsswd)
        {
            var user = new User(userName, userPsswd, Rol.Regular);
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
