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
        private readonly UsersAdminPage view;
        private User? user;

        public UsersAdminController()
        {
            userRepository = new UserRepository();
        }

        public UsersAdminController(UsersAdminPage view) : this()
        {
            this.view = view;
            view.rol.ItemsSource = Enum.GetValues<Rol>();
        }

        /// <summary>
        /// Busca un usuario por su nombre.
        /// Puede devolver null, si el nombre de usuario no existe
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private async Task<User?> SearchUser(string userName)
        {
            var user = (await userRepository
                .ReadAll())
                .FirstOrDefault(x => x.Name.Equals(userName));
            return user;
        }

        private async Task Delete(User user)
        {
            await userRepository.Delete(user);
        }

        public async Task Buscar()
        {
            var userName = view.nombreBusqueda.Text;
            user = await SearchUser(userName);
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
            user.Rol = newRol;
            await userRepository.Update(user);
        }
    }
}
