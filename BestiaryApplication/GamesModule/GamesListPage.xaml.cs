using BestiaryApplication.Common.utils;
using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BestiaryApplication.GamesModule
{
    public partial class GamesListPage : Page
    {
        private Point startingMousePosition;

        public GamesListPage()
        {
            InitializeComponent();
        }

        private void SetRPGGenre(object sender, RoutedEventArgs e)
        {
            genre_selector.Tag = "RPG";
        }

        private void SetActionRPGGenre(object sender, RoutedEventArgs e)
        {
            genre_selector.Tag = "Action-RPG";
        }

        private void OnListViewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                startingMousePosition = Mouse.GetPosition(games_list_view);
            }
        }

        private void OnListViewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                
                // Get the current position and the delta
                var currentPosition = e.GetPosition(games_list_view);
                var delta = currentPosition.Y - startingMousePosition.Y;

                // Get the scroll viewer and its offset
                var scrollViewer = GeneralMethods.FindVisualChild<ScrollViewer>(games_list_view);
                var offset = scrollViewer.VerticalOffset;

                // Scroll by the delta amount
                scrollViewer.ScrollToVerticalOffset(offset - delta);

                // Update the start position
                startingMousePosition = currentPosition;
            }
        }
    }
}
