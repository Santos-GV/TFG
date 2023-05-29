using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Controllers;
using WpfAppTFG.Model;
using WpfAppTFG.Views.Controls;

namespace WpfAppTFG.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para PostsPage.xaml
    /// </summary>
    public partial class PostsPage : Page
    {
        public delegate void AbrirPostEvento(Post post);
        public event AbrirPostEvento abrirPostEvento;
        private readonly PostController controller;
        private IEnumerator<Lazy<IEnumerable<Post>>> postsEnumerator;
        private List<string> tags;

        public PostsPage()
        {
            InitializeComponent();
        }

        public PostsPage(User user) : this()
        {
            controller = new PostController(user);
            LoadPosts().Wait();
            tags = new List<string>();
        }

        private async Task LoadPosts()
        {
            var posts = await controller.ReadAllPostPagedLazy();
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
                var currentposts = postsEnumerator.Current.Value;
                foreach (var post in currentposts)
                {
                    var etiquetas = post.Etiquetas.AsEnumerable();
                    if (!ContainsAllTags(etiquetas)) return;
                    var control = new PostsControl(post);
                    control.clickEvento += () => abrirPostEvento(post);
                    control.clickFavoritosEvento += async () => await addFavoritos(post);
                    control.clickPendientesEvento += async () => await addPendientes(post);
                    postsContainer.Children.Add(control);
                }
            }
        }

        private bool ContainsAllTags(IEnumerable<string> etiquetas)
        {
            if (isEmpty(tags)) return true; // si no hay etiquetas para filtrar, se admiten todos los posts
            if (isEmpty(etiquetas)) return false; // si hay etiquetas que filtrar y el post no tiene, no es válido
            // Las comprobaciones anteriores son porque este código no es válido en el caso en que una de las listas está vacía
            return isEmpty(etiquetas.Except(tags));
        }

        private bool isEmpty<T>(IEnumerable<T> ienumerable)
        {
            return !ienumerable.Any();
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

        private async void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            await LoadPosts();
            postsContainer.Children.Clear();
            expFiltrar.IsExpanded = false;
        }

        private async Task addFavoritos(Post post)
        {
            await controller.addFavoritos(post);
        }

        private async Task addPendientes(Post post)
        {
            await controller.addPendientes(post);
        }
    }
}
