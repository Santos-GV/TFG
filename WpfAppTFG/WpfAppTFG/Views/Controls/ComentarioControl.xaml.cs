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
        private readonly Comentario comentario;
        private readonly User user;
        private readonly ComentarioController controller;

        public ComentarioControl()
        {
            InitializeComponent();
            controller = new ComentarioController(this);
        }

        public ComentarioControl(Comentario comentario, User user) : this()
        {
            this.comentario = comentario;
            this.user = user;
            SetContent();
        }

        private void SetContent()
        {
            controller.SetContent(comentario, user);
        }

        private async void btnEliminar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await controller.Eliminar(comentario);
        }
    }
}
