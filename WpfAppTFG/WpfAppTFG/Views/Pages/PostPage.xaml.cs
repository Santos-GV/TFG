using System;
using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Controllers;
using WpfAppTFG.Model;
using WpfAppTFG.Views.Controls;

namespace WpfAppTFG.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para PostPage.xaml
    /// </summary>
    public partial class PostPage : Page
    {
        public delegate void PostEliminadoEvento();
        public event PostEliminadoEvento postEliminadoEvento;
        private readonly PostController controller;

        public PostPage()
        {
            InitializeComponent();
            controller = new PostController(this);
        }

        public PostPage(Post post, User user) : this()
        {
            controller.SetContext(post, user);
        }

        private async void btnComentar_Click(object sender, RoutedEventArgs e)
        {
            await controller.AddComentario();
        }

        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            await controller.EliminarPost();
            postEliminadoEvento();
        }
    }
}
