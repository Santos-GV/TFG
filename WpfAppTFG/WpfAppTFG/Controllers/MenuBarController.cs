using System;
using System.Windows;
using WpfAppTFG.Model;
using WpfAppTFG.Views.Controls;

namespace WpfAppTFG.Controllers
{
    public class MenuBarController
    {
        private readonly MenuBarControl view;
        private User user;

        public MenuBarController(MenuBarControl menuBarControl)
        {
            this.view = menuBarControl;
        }

        public void SetContext(User user)
        {
            this.user = user;
            view.administrarUsuarios.Visibility = user.Rol switch
            {
                Rol.Regular => Visibility.Collapsed,
                Rol.Moderador => Visibility.Collapsed,
                Rol.Administrador => Visibility.Visible,
                _ => throw new ArgumentOutOfRangeException($"Rol de usuario inesperado `{user.Rol}`")
            };
        }

        public void UpdateNotificaciones(bool hasNotifications)
        {
            var notificationIndicator = hasNotifications ? "*" : "";
            view.miMisPosts.Header = $"Mis posts{notificationIndicator}";
        }
    }
}
