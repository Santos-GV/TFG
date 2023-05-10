using System.Windows;
using System.Windows.Controls;
using WpfAppTFG.Model;
using WpfAppTFG.Views.Shareds;

namespace WpfAppTFG.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para CreatePostPage.xaml
    /// </summary>
    public partial class CreatePostPage : Page
    {
        public CreatePostPage()
        {
            InitializeComponent();
        }

        public CreatePostPage(User user) : this()
        {
            control.SetUser(user);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            WindowProperties.SetWindowTitle("Crear Post", this);
        }
    }
}
