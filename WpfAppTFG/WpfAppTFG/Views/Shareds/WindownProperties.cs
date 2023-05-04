using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace WpfAppTFG.Views.Shareds
{
    public class WindowProperties
    {
        public static void SetWindowOwner(Window childWindow, Page page)
        {
            // Obtener la NavigationWindow que contiene esta página
            var navigationWindow = GetParentWindow(page);

            // Si existe una NavigationWindow, establece su propiedad Title
            if (navigationWindow != null)
            {
                childWindow.Owner = navigationWindow;
            }
        }

        public static void SetWindowVisibility(Visibility visibility, Page page)
        {
            // Obtener la NavigationWindow que contiene esta página
            var navigationWindow = GetParentWindow(page);

            // Si existe una NavigationWindow, establece su propiedad Title
            if (navigationWindow != null)
            {
                navigationWindow.Visibility = visibility;
            }
        }

        public static void SetWindowTitle(string title, Page page)
        {
            // Obtener la NavigationWindow que contiene esta página
            var navigationWindow = GetParentWindow(page);

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
