using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
using BestiaryApplication.Common.SharedControls.Windows;
using System.Windows.Controls;

namespace BestiaryApplication.Common.utils
{
    class GeneralMethods
    {
        public static string GetImagePath()
        {
            Microsoft.Win32.OpenFileDialog dlg = new()
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp) | *.jpg; *.jpeg; *.png; *.bmp"
            };

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

        public static byte[] ConvertToBytes(BitmapImage bitmapImage)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

            using (MemoryStream ms = new())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
        }

        public static BitmapImage LoadImage(string path)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;

            if (!string.IsNullOrEmpty(path)) image.UriSource = new(path);
            else image.UriSource = new("ControlIcons/image_control_placeholder.png", UriKind.Relative);
            
            image.EndInit();

            return image;
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

        public static void ZoomImage(ImageSource imageSource)
        {
            var displayWindow = new ImageDisplayWindow();

            var image = (Image)displayWindow.FindName("zoomed_image");
            image.Source = imageSource;

            displayWindow.ShowDialog();
        }
    }
}
