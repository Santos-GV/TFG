﻿using System.Windows.Controls;
using WpfAppTFG.Model;

namespace WpfAppTFG.Views.Controls
{
    /// <summary>
    /// Lógica de interacción para PostsControl.xaml
    /// </summary>
    public partial class PostsControl : UserControl
    {
        public delegate void ClickEvento();
        public event ClickEvento clickEvento;
        public delegate void ClickFavoritosEvento();
        public event ClickFavoritosEvento clickFavoritosEvento;
        public delegate void ClickPendientesEvento();
        public event ClickPendientesEvento clickPendientesEvento;

        public Post post { get; }
        public PostsControl()
        {
            InitializeComponent();
        }

        public PostsControl(Post post) : this()
        {
            this.post = post;
            title.Text = post.Titulo;
        }

        private void UserControl_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            clickEvento();
        }

        private void MenuItem_Favoritos_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            clickFavoritosEvento();
        }

        private void MenuItem_Pendientes_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            clickPendientesEvento();
        }
    }
}
