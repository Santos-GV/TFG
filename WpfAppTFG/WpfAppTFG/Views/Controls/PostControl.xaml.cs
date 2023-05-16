using System;
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
            //stpEtiquetas.Text = post.Content;
        }

        private void btnVerComentarios_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void btnComentar_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
