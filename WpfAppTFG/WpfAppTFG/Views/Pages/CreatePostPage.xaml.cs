using System.Windows.Controls;
using WpfAppTFG.Model;

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
    }
}
