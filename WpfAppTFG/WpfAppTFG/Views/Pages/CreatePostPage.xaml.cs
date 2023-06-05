using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Controllers;
using WpfAppTFG.Model;

namespace WpfAppTFG.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para CreatePostPage.xaml
    /// </summary>
    public partial class CreatePostPage : Page
    {
        public delegate void PostCreadoEvento();
        public event PostCreadoEvento postCreadoEvento;
        private CreatePostController? controller;

        public CreatePostPage()
        {
            InitializeComponent();
        }

        public CreatePostPage(User user) : this()
        {
            controller = new CreatePostController(user, this);
        }

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            if (controller is null) return;
            controller.AddEtiqueta();
        }

        private async Task Guardar()
        {
            if (controller is null) return;
            await controller.CreatePost();
            postCreadoEvento();

        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            await Guardar();
        }

        private async void AtajoGuardar(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            await Guardar();
        }
    }
}
