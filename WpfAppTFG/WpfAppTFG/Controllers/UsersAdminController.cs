using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfAppTFG.Model;
using WpfAppTFG.Model.Respository;
using WpfAppTFG.Views.Pages;

namespace WpfAppTFG.Controllers
{
    public class UsersAdminController
    {
        private readonly UserRepository userRepository;
        private readonly LogRepository logRepository;
        private readonly UsersAdminPage view;
        private readonly User userEditor;
        private User? user;

        public UsersAdminController()
        {
            userRepository = new UserRepository();
            logRepository = new LogRepository();
        }

        public UsersAdminController(UsersAdminPage view, User user) : this()
        {
            this.view = view;
            this.userEditor = user;
            view.rol.ItemsSource = Enum.GetValues<Rol>();
        }

        /// <summary>
        /// Busca un usuario por su nombre.
        /// Puede devolver null, si el nombre de usuario no existe
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private User? SearchUser(string userName)
        {
            var user = userRepository
                .ReadAll()
                .FirstOrDefault(x => x.Name.Equals(userName));
            return user;
        }

        private async Task Delete(User user)
        {
            var userTask = userRepository.Delete(user);
            var log = new Log(userEditor.Id, $"Elimina el usuario `{user.Id}`");
            var logTask = logRepository.Create(log);
            await Task.WhenAll(userTask, logTask);
        }

        public void Buscar()
        {
            var userName = view.nombreBusqueda.Text;
            user = SearchUser(userName);
            if (user == null)
            {
                view.info.Content = "El usuario no existe";
                return;
            }
            view.nombre.Text = user.Name;
            view.rol.SelectedItem = user.Rol;
        }

        public async Task Eliminar()
        {
            const string message =
@"¿Estás seguro?
Está accicion no se puede deshacer";
            var result = MessageBox.Show(message, "Eliminar usuario", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.No) return;
            if (user == null) return;
            await Delete(user);
            view.nombreBusqueda.Text = string.Empty;
            view.nombre.Text = string.Empty;
            view.rol.SelectedItem = null;
        }

        public async Task CambiarRol()
        {
            var selectedRol = view.rol.SelectedItem?.ToString();
            if (selectedRol == null) return;
            var isParsed = Enum.TryParse(selectedRol, out Rol newRol);
            if (!isParsed) return;
            if (user == null) return;
            var oldRol = user.Rol;
            if (oldRol == newRol) return;
            user.Rol = newRol;
            var userTask = userRepository.Update(user);
            var log = new Log(user.Id, $"Cambia el rol del usuario `{user.Id}` de `{oldRol}` a `{user.Rol}`");
            var logTask = logRepository.Create(log);
            await Task.WhenAll(userTask, logTask);
        }
    }
}
