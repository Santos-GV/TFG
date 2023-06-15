using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using WpfAppTFG.Model;
using WpfAppTFG.Model.Respository;
using WpfAppTFG.Views.Controls;
using WpfAppTFG.Views.Pages;
using System.Windows.Media;

namespace WpfAppTFG.Controllers
{
    public class PostsController
    {
        private readonly PostRepository postRepository;
        private readonly UserRepository userRepository;
        private readonly LogRepository logRepository;
        private readonly PostsPage view;
        private readonly User user;
        private IEnumerator<Lazy<IEnumerable<Post>>> postsEnumerator;
        private List<string> tags;

        public PostsController(PostsPage view, User user)
        {
            this.postRepository = new PostRepository();
            this.userRepository = new UserRepository();
            this.logRepository = new LogRepository();
            this.view = view;
            this.user = user;
            this.tags = new List<string>();
        }

        private async Task<IEnumerable<Lazy<IEnumerable<Post>>>> ReadAllPostPagedLazy()
        {
            const int pageSize = 20;
            var posts = await postRepository.ReadAllPagedLazy(pageSize);
            return posts;
        }

        private async Task addFavoritos(Post post)
        {
            if (user.Favoritos.Contains(post.Id)) return;
            user.Favoritos.Add(post.Id);
            var userTask = userRepository.Update(user);
            var log = new Log(user.Id, $"Añade a favoritos el post `{post.Id}`");
            var logTask = logRepository.Create(log);
            await Task.WhenAll(userTask, logTask);
        }

        private async Task addPendientes(Post post)
        {
            if (user.Pendientes.Contains(post.Id)) return;
            user.Pendientes.Add(post.Id);
            var userTask = userRepository.Update(user);
            var log = new Log(user.Id, $"Añade a pendientes el post `{post.Id}`");
            var logTask = logRepository.Create(log);
            await Task.WhenAll(userTask, logTask);
        }

        public async Task LoadPosts()
        {
            var posts = await ReadAllPostPagedLazy();
            postsEnumerator = posts.GetEnumerator();
        }

        public void LoadNextPosts()
        {
            int count = 0;
            // Si no se encuentran posts con las etiquetas filtradas, se pasa al siguiente bloque del Enumerator
            while (count != -1 && count == 0)
            {
                count = 0;
                if (postsEnumerator.MoveNext())
                {
                    var currentposts = postsEnumerator.Current.Value;
                    foreach (var post in currentposts)
                    {
                        var etiquetas = post.Etiquetas.AsEnumerable();
                        if (!ContainsAllTags(etiquetas)) continue;
                        var control = new PostsControl(post);
                        control.clickEvento += () => view.abrirPost(post);
                        control.clickFavoritosEvento += async () => await addFavoritos(post);
                        control.clickPendientesEvento += async () => await addPendientes(post);
                        view.postsContainer.Children.Add(control);
                        count++;
                    }
                }
                else
                {
                    count = -1;
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

        public void AddTag()
        {
            var etiqueta = view.txtEtiqueta.Text;
            view.txtEtiqueta.Text = string.Empty;

            var control = new TextBlock();
            control.Text = etiqueta;
            control.Margin = new Thickness(8);
            control.Style = (Style)Application.Current.Resources["etiqueta"];

            tags.Add(etiqueta);
            view.stpEtiquetas.Children.Add(control);
        }

        public async Task Filtrar()
        {
            view.postsContainer.Children.Clear();
            await LoadPosts();
            view.expFiltrar.IsExpanded = false;
        }
    }
}
