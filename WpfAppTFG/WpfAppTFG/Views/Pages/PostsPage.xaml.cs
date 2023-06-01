using System;
using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Controllers;
using WpfAppTFG.Model;

namespace WpfAppTFG.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para PostsPage.xaml
    /// </summary>
    public partial class PostsPage : Page
    {
        public delegate void AbrirPostEvento(Post post);
        public event AbrirPostEvento abrirPostEvento;
        private readonly PostsController controller;

        public PostsPage()
        {
            InitializeComponent();
        }

        public PostsPage(User user) : this()
        {
            controller = new PostsController(this, user);
            controller.LoadPosts().Wait();
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            // Cuando llegue al final del scroll
            if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
            {
                controller.LoadNextPosts();
            }
        }

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            controller.AddTag();
        }

        private async void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            await controller.Filtrar();
        }

        public void abrirPost(Post post)
        {
            abrirPostEvento(post);
        }
    }
}
