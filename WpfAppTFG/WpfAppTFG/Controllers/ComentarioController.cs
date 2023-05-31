using System;
using System.Threading.Tasks;
using System.Windows;
using WpfAppTFG.Model;
using WpfAppTFG.Model.Respository;
using WpfAppTFG.Views.Controls;

namespace WpfAppTFG.Controllers
{
    public class ComentarioController
    {
        private readonly ComentarioRepository comentarioRepository;
        private readonly UserRepository userRepository;
        private readonly ComentarioControl view;

        public ComentarioController(ComentarioControl view)
        {
            this.comentarioRepository = new ComentarioRepository();
            this.userRepository = new UserRepository();
            this.view = view;
        }

        internal async Task Eliminar(Comentario comentario)
        {
            var result = MessageBox.Show($"Quiere eliminar el comentario `{comentario.Id}`?", "Peligro", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No) return;
            await comentarioRepository.Delete(comentario);
            MessageBox.Show($"Post `{comentario.Id} eliminado`", "Peligro", MessageBoxButton.OK, MessageBoxImage.Information);
            view.Visibility = Visibility.Collapsed;
        }

        internal void SetContent(Comentario comentario, User user)
        {
            var commentUser = userRepository.Read(comentario.IdUsuario);
            view.usuario.Text = commentUser?.Name;
            view.contenido.Text = comentario.Contenido;
            view.btnEliminar.Visibility = user.Rol switch
            {
                Rol.Regular => Visibility.Collapsed,
                Rol.Moderador => Visibility.Visible,
                Rol.Administrador => Visibility.Visible,
                _ => throw new ArgumentOutOfRangeException($"Rol de usuario inesperado `{user.Rol}`")
            };
        }
    }
}
