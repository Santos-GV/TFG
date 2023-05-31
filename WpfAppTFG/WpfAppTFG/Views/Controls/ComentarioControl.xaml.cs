using System.Threading.Tasks;
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

        public ComentarioControl(User user)
        {
            InitializeComponent();
            controller = new ComentarioController(this, user);
        }

        public ComentarioControl(Comentario comentario, User user) : this(user)
        {
            this.comentario = comentario;
            this.user = user;
            SetContent().Wait();
        }

        private async Task SetContent()
        {
            await controller.SetContent(comentario, user);
        }

        private async void btnEliminar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await controller.Eliminar(comentario);
        }
    }
}
