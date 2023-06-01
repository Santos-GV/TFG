using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfAppTFG.Model;
using WpfAppTFG.Model.Respository;
using WpfAppTFG.Views.Pages;
using WpfAppTFG.Views.Shareds;
using WpfAppTFG.Views.Windows;

namespace WpfAppTFG.Controller
{
    public class LoginController
    {
        private readonly UserRepository userRepository;
        private readonly LogRepository logRepository;
        public User? logginUser;
        private readonly LoginPage view;

        public LoginController(LoginPage view)
        {
            userRepository = new UserRepository();
            logRepository = new LogRepository();
            this.view = view;
        }

        /// <summary>
        /// Intenta logear el usuario
        /// </summary>
        /// <returns></returns>
        public async Task TryLogin()
        {
            var userTask = Login(view.userName.Text, view.psswd.Password);
            if (string.IsNullOrEmpty(view.userName.Text) || string.IsNullOrEmpty(view.psswd.Password))
            {
                view.info.Text = "Rellena todos los campos";
                return;
            }
            var user = await userTask;
            if (user is not null)
            {
                logginUser = user;
                view.info.Text = string.Empty;
                view.userName.Text = string.Empty;
                view.psswd.Password = string.Empty;
                await Login();
            }
            else
            {
                view.info.Text = "El usuario y/o contraseña no son válidos";
                view.psswd.Password = string.Empty;
            }
        }

        /// <summary>
        /// Abre la ventana de inicio
        /// </summary>
        private async Task Login()
        {
            WindowProperties.SetWindowVisibility(Visibility.Hidden, view);
            var user = logginUser;
            if (user is null)
            {
                MessageBox.Show("No se pudo abrir la aplicación", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var log = new Log(user.Id, $"Hace login");
            await logRepository.Create(log);
            var homeWindow = new HomeWindow(user);
            WindowProperties.SetWindowOwner(homeWindow, view);
            homeWindow.Show();
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

        /// <summary>
        /// Cambia el título de la ventana
        /// </summary>
        public void ChangeWindowTitle()
        {
            WindowProperties.SetWindowTitle("Login", view);
        }

        /// <summary>
        /// Navega a la página de regitro
        /// </summary>
        public void Registrarse()
        {
            var registerPage = new RegisterPage();
            view.NavigationService.Navigate(registerPage);
        }
    }
}
