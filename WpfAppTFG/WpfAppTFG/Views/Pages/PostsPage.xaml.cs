using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Model;
using WpfAppTFG.Views.Shareds;

namespace WpfAppTFG.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para PostsPage.xaml
    /// </summary>
    public partial class PostsPage : Page
    {
        public PostsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO: Fix
            WindowProperties.SetWindowTitle(Title, this);
        }

        private void postsContainer_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // TODO: Open post
            var post = (e.Source as Post);
        }
    }
}
