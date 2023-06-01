using System.Windows.Controls;
using WpfAppTFG.Controllers;
using WpfAppTFG.Model;

namespace WpfAppTFG.Views.Controls
{
    /// <summary>
    /// Lógica de interacción para ComentarioControl.xaml
    /// </summary>
    public partial class ComentarioControl : UserControl
    {
        private readonly ComentarioController controller;

        public ComentarioControl()
        {
            InitializeComponent();
            controller = new ComentarioController(this);
        }

        public ComentarioControl(Comentario comentario, User user) : this()
        {
            SetContent(comentario, user);
        }

        private void SetContent(Comentario comentario, User user)
        {
            controller.SetContent(comentario, user);
        }

        private async void btnEliminar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await controller.Eliminar();
        }
    }
}
