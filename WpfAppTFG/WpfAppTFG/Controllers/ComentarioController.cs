using System;
using System.Runtime.CompilerServices;
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
        private readonly LogRepository logRepository;
        private readonly ComentarioControl view;
        private Comentario comentario;
        private User user;

        public ComentarioController(ComentarioControl view)
        {
            this.comentarioRepository = new ComentarioRepository();
            this.userRepository = new UserRepository();
            this.logRepository = new LogRepository();
            this.view = view;
        }

        public async Task Eliminar()
        {
            var result = MessageBox.Show($"Quiere eliminar el comentario `{comentario.Id}`?", "Peligro", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No) return;
            var comentarioTask = comentarioRepository.Delete(comentario);
            var log = new Log(user.Id, $"Elimina el comentario {comentario.Id}");
            var logTask = logRepository.Create(log);
            await Task.WhenAll(comentarioTask, logTask);
            MessageBox.Show($"Post `{comentario.Id} eliminado`", "Peligro", MessageBoxButton.OK, MessageBoxImage.Information);
            view.Visibility = Visibility.Collapsed;
        }

        public void SetContent(Comentario comentario, User user)
        {
            this.comentario = comentario;
            this.user = user;
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
