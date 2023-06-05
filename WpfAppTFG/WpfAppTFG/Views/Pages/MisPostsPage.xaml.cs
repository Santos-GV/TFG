using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfAppTFG.Controllers;
using WpfAppTFG.Model;
using WpfAppTFG.Views.Controls;

namespace WpfAppTFG.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para MisPostsPage.xaml
    /// </summary>
    public partial class MisPostsPage : Page
    {
        public delegate void AbrirPostEvento(Post post);
        public event AbrirPostEvento abrirPostEvento;
        public delegate void CrearPostEvento();
        public event CrearPostEvento crearPostEvento;
        private readonly MisPostsController controller;
        private IEnumerator<Post> postsEnumerator;

        public MisPostsPage()
        {
            InitializeComponent();
        }

        public MisPostsPage(User user) : this()
        {
            controller = new MisPostsController(user);
            LoadPosts();
        }

        private void LoadPosts()
        {
            var posts = controller.ReadAllPost();
            postsEnumerator = posts.GetEnumerator();
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            // Cuando llegue al final del scroll
            if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
            {
                LoadNextPosts();
            }
        }

        private void LoadNextPosts()
        {
            if (postsEnumerator.MoveNext())
            {
                var currentpost = postsEnumerator.Current;
                var etiquetas = currentpost.Etiquetas.AsEnumerable();
                var control = new PostsControl(currentpost);
                control.clickEvento += () => abrirPostEvento(currentpost);
                control.clickFavoritosEvento += async () => await addFavoritos(currentpost);
                control.clickPendientesEvento += async () => await addPendientes(currentpost);
                postsContainer.Children.Add(control);
            }
        }
        private async Task addFavoritos(Post post)
        {
            await controller.addFavoritos(post);
        }

        private async Task addPendientes(Post post)
        {
            await controller.addPendientes(post);
        }

        private void btnCrearPost_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            crearPostEvento();
        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {

        }
    }
}
