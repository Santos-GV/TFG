using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Model;
using WpfAppTFG.Model.Respository;
using WpfAppTFG.Views.Pages;

namespace WpfAppTFG.Controllers
{
    public class CreatePostController
    {
        private readonly PostRepository postRepository;
        private readonly User user;
        private List<string> tags;
        private readonly CreatePostPage view;

        public CreatePostController(User user, CreatePostPage view)
        {
            this.postRepository = new PostRepository();
            this.tags = new List<string>();
            this.user = user;
            this.view = view;
        }

        public async Task CreatePost()
        {
            var title = view.txtTitulo.Text;
            var content = view.txtContenido.Text;
            var post = new Post(user.Id, title, content, tags);
            await postRepository.Create(post);
            MessageBox.Show($"Post `{title}` creado con exito", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            // Limpia el contenido depues de crear el post
            view.txtTitulo.Text = string.Empty;
            view.txtContenido.Text = string.Empty;
            tags.Clear();
            view.stpEtiquetas.Children.Clear();
        }

        public void AddEtiqueta()
        {
            var etiqueta = view.txtEtiqueta.Text;
            view.txtEtiqueta.Text = string.Empty;

            var control = new TextBlock();
            control.Text = etiqueta;
            control.Margin = new Thickness(8);
            control.Style = (Style)view.Resources["text-block"];

            tags.Add(etiqueta);
            view.stpEtiquetas.Children.Add(control);
        }
    }
}
