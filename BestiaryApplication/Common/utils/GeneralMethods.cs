using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace BestiaryApplication.Common.utils
{
    class GeneralMethods
    {
        public static string GetImagePath()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp) | *.jpg; *.jpeg; *.png; *.bmp";

            if (dlg.ShowDialog() == true) return dlg.FileName;
            else return "";
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj)
        where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static T FindVisualChild<T>(DependencyObject obj)
        where T : DependencyObject
        {
            foreach (T child in FindVisualChildren<T>(obj))
            {
                return child;
            }
            return null;
        }
    }
}
