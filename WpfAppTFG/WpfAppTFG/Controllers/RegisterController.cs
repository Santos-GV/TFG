using System;
using System.Threading.Tasks;
using WpfAppTFG.Model;
using WpfAppTFG.Model.Exception;
using WpfAppTFG.Model.Respository;
using WpfAppTFG.Views.Pages;
using WpfAppTFG.Views.Shareds;

namespace WpfAppTFG.Controller
{
    public class RegisterController
    {
        private readonly UserRepository userRepository;
        private readonly LogRepository logRepository;
        private readonly RegisterPage view;

        public RegisterController(RegisterPage view)
        {
            userRepository = new UserRepository();
            logRepository = new LogRepository();
            this.view = view;
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
                var userTask = userRepository.Create(user);
                var log = new Log(user.Id, "Se registra el usuario");
                var logTask = logRepository.Create(log);
                await Task.WhenAll(userTask, logTask);
                return true;
            }
            catch (UserAlreadyExists)
            {
                return false;
            }
        }

        public void ChangeWindowTitle()
        {
            WindowProperties.SetWindowTitle("Register", view);
        }

        public void NavigateLogin()
        {
            var loginPage = new LoginPage();
            view.NavigationService.Navigate(loginPage);
        }

        public async Task Register()
        {

            var userName = view.user.Text;
            var psswd1 = view.psswd1.Password;
            var psswd2 = view.psswd2.Password;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(psswd1) || string.IsNullOrEmpty(psswd2))
            {
                view.info.Text = "Rellena todos los campos";
                return;
            }
            if (!psswd1.Equals(psswd2))
            {
                view.info.Text = "Las contraseñas no coinciden";
                return;
            }
            var isRegistered = await Register(userName, psswd1);
            if (isRegistered)
            {
                NavigateLogin();
            }
            else
            {
                view.info.Text = "No se a podido crear la cuenta usuario";
                return;
            }
        }
    }
}
