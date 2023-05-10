using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Controllers;
using WpfAppTFG.Model;

namespace WpfAppTFG.Views.Controls
{
    /// <summary>
    /// Lógica de interacción para CreatePostControl.xaml
    /// </summary>
    public partial class CreatePostControl : UserControl
    {
        private CreatePostController? controller;
        private List<string> tags;

        public CreatePostControl()
        {
            InitializeComponent();
            tags = new List<string>();
        }

        public void SetUser(User user)
        {
            controller = new CreatePostController(user);
        }

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            var etiqueta = txtEtiqueta.Text;
            txtEtiqueta.Text = string.Empty;

            var control = new TextBlock();
            control.Text = etiqueta;
            control.Margin = new Thickness(8);
            control.Style = (Style)Resources["text-block"];

            tags.Add(etiqueta);
            stpEtiquetas.Children.Add(control);
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (controller is null) return;
            var title = txtTitulo.Text;
            var content = txtContenido.Text;
            await controller.CreatePost(title, content, tags);
            // Limpia el contenido depues de crear el post
            txtTitulo.Text = string.Empty;
            txtContenido.Text = string.Empty;
            tags.Clear();
            stpEtiquetas.Children.Clear();
        }
    }
}
