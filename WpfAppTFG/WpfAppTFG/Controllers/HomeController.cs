using System;
using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Model;
using WpfAppTFG.Views.Pages;
using WpfAppTFG.Views.Windows;

namespace WpfAppTFG.Controllers
{
    public class HomeController
    {
        private HomeWindow view;
        private User user;

        public HomeController(HomeWindow homeWindow, User user)
        {
            this.view = homeWindow;
            this.user = user;
            view.menu.SetContext(user);
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
            // Cierra la aplicación entera
            // tanto la ventana padre, como esta
            Application.Current.Shutdown();
        }

        public void CerrarSesion()
        {
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
    }
}
