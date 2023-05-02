using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace WpfAppTFG.Views.Shareds
{
    public class WindownProperties
    {
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
