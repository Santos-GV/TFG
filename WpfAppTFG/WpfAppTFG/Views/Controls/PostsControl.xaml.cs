using System.Windows.Controls;
using WpfAppTFG.Model;

namespace WpfAppTFG.Views.Controls
{
    /// <summary>
    /// Lógica de interacción para PostsControl.xaml
    /// </summary>
    public partial class PostsControl : UserControl
    {
        public Post post { get; }
        public PostsControl()
        {
            InitializeComponent();
        }

        public PostsControl(Post post) : this()
        {
            this.post = post;
            title.Text = post.Titulo;
        }
    }
}
