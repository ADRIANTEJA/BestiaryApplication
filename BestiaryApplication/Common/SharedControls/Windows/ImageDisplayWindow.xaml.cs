using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BestiaryApplication.Common.SharedControls.Windows
{
    /// <summary>
    /// Interaction logic for ImageDisplayWindow.xaml
    /// </summary>
    public partial class ImageDisplayWindow : Window
    {
        public ImageDisplayWindow()
        {
            InitializeComponent();
        }

        private void OnQuitButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MoveMouse(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) { DragMove(); }
        }

        private void OnEsckeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) { Close(); }
        }
    }
}
