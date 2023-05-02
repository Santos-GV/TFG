using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace WpfAppTFG.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            SetWindowTitle("Login");
        }

        private void login_registrarseEvento()
        {
            var registerPage = new RegisterPage();
            this.NavigationService.Navigate(registerPage);
        }

        private void SetWindowTitle(string title)
        {
            // Obtener la NavigationWindow que contiene esta página
            var navigationWindow = GetParentWindow(this);

            // Si existe una NavigationWindow, establece su propiedad Title
            if (navigationWindow != null)
            {
                navigationWindow.Title = title;
            }
        }

        private static NavigationWindow? GetParentWindow(DependencyObject child)
        {
            // Recorrer el árbol visual para encontrar la NavigationWindow que contiene esta página
            var parent = VisualTreeHelper.GetParent(child);

            while (parent != null && !(parent is NavigationWindow))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as NavigationWindow;
        }
    }
}
