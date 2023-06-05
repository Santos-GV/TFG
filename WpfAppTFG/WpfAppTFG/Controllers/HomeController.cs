using MongoDB.Driver.Linq;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WpfAppTFG.Model;
using WpfAppTFG.Model.Respository;
using WpfAppTFG.Views.Pages;
using WpfAppTFG.Views.Windows;

namespace WpfAppTFG.Controllers
{
    public class HomeController
    {
        private HomeWindow view;
        private readonly PostRepository postRepository;
        private DispatcherTimer hiloNotificaciones;
        private int? comentariosNum;
        private User user;

        public HomeController(HomeWindow homeWindow, User user)
        {
            this.view = homeWindow;
            this.user = user;
            this.postRepository = new PostRepository();
            CreateNotificationsThread();
            view.menu.SetContext(user);
        }

        private void CreateNotificationsThread()
        {
            hiloNotificaciones = new DispatcherTimer();
            hiloNotificaciones.Interval = TimeSpan.FromMinutes(2);
            hiloNotificaciones.Tick += CheckNotifications;
            hiloNotificaciones.Start();
        }

        private void CheckNotifications(object? sender, EventArgs e)
        {
            var comentariosNum = postRepository.ReadAll()
                .Where(post => post.IdUsuario.Equals(user.Id))
                .SelectMany(post => post.Comentarios)
                .Count();
            // La primera vez comentariosNum no está inicializado
            if (this.comentariosNum == null)
            {
                this.comentariosNum = comentariosNum;
                return;
            }
            if (this.comentariosNum < comentariosNum)
            {
                this.comentariosNum = comentariosNum;
                view.menu.UpdateNotificaciones(true);
                MessageBox.Show("¡Tienes nuevos comentarios en tus posts!");
            }
        }

        public void PageNavigated()
        {
            var page = view.pagesContainer.Content as Page;
            if (page == null)
            {
                view.Title = "Home";
                return;
            }
            view.Title = page.Title;
        }

        private void OpenPost(Post post)
        {
            var postPage = new PostPage(post, user);
            postPage.postEliminadoEvento += NavigateHome;
            view.pagesContainer.Navigate(postPage);
        }

        private void NavigateHome()
        {
            view.pagesContainer.Content = null;
        }

        public void NavigatePosts()
        {
            var postsPage = new PostsPage(user);
            postsPage.abrirPostEvento += OpenPost;
            view.pagesContainer.Navigate(postsPage);
        }

        public void NavigateAdministrarUsuarios()
        {
            var usersAdminPage = new UsersAdminPage(user);
            view.pagesContainer.Navigate(usersAdminPage);
        }

        public void NavigateAcercaDe()
        {
            var acerdaDeWindow = new AcercaDeWindow();
            acerdaDeWindow.Owner = view;
            acerdaDeWindow.ShowDialog();
        }

        public void Salir()
        {
            hiloNotificaciones.Stop();
            // Cierra la aplicación entera
            // tanto la ventana padre, como esta
            Application.Current.Shutdown();
        }

        public void CerrarSesion()
        {
            hiloNotificaciones.Stop();
            try
            {
                view.Owner.Visibility = Visibility.Visible;
                view.Close();
            }
            catch (InvalidOperationException) { }
        }

        public void NavigateCrearPost()
        {
            var crearPostPage = new CreatePostPage(user);
            crearPostPage.postCreadoEvento += NavigateHome;
            view.pagesContainer.Navigate(crearPostPage);
        }

        public void NavigateFavoritos()
        {
            var favoritosPage = new FavoritosPage(user);
            favoritosPage.abrirPostEvento += OpenPost;
            view.pagesContainer.Navigate(favoritosPage);
        }

        public void NavigatePendientes()
        {
            var pendientesPage = new PendientesPage(user);
            pendientesPage.abrirPostEvento += OpenPost;
            view.pagesContainer.Navigate(pendientesPage);
        }

        internal void NavigateMisPosts()
        {
            var misPostsPage = new MisPostsPage(user);
            misPostsPage.abrirPostEvento += OpenPost;
            misPostsPage.crearPostEvento += NavigateCrearPost;
            view.menu.UpdateNotificaciones(); // Se quita el inficador de notificaciones pendientes
            view.pagesContainer.Navigate(misPostsPage);
        }
    }
}
