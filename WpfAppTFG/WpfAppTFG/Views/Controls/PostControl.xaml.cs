using System;
using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Model;

namespace WpfAppTFG.Views.Controls
{
    /// <summary>
    /// Lógica de interacción para PostControl.xaml
    /// </summary>
    public partial class PostControl : UserControl
    {
        public PostControl()
        {
            InitializeComponent();
        }

        public void SetContext(Post post)
        {
            txtTitulo.Text = post.Titulo;
            txtContenido.Text = post.Contenido;
            foreach (var etiqueta in post.Etiquetas)
            {
                var control = new TextBlock();
                control.Text = etiqueta;
                control.Margin = new Thickness(8);
                control.Style = (Style)Resources["text-block"];

                stpEtiquetas.Children.Add(control);
            }
        }

        private void btnVerComentarios_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void btnComentar_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
