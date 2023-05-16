using System;
using System.Windows.Controls;
using WpfAppTFG.Model;

namespace WpfAppTFG.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para PostPage.xaml
    /// </summary>
    public partial class PostPage : Page
    {
        public PostPage()
        {
            InitializeComponent();
        }

        public PostPage(Post post) : this()
        {
            this.post.SetContext(post);
        }
    }
}
