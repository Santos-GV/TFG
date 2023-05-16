using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Controllers;
using WpfAppTFG.Model;
using WpfAppTFG.Views.Controls;
using WpfAppTFG.Views.Shareds;

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

        public PostsPage()
        {
            InitializeComponent();
            controller = new PostController();
            postsEnumerator = controller.ReadAllPostPagedLazy().Result.GetEnumerator();
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
                    var control = new PostsControl(post);
                    control.clickEvento += () => abrirPostEvento(post);
                    postsContainer.Children.Add(control);
                }
            }
        }
    }
}
