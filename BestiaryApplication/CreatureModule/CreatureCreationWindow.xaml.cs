using System.Windows;
using System.Windows.Input;

namespace BestiaryApplication.GamesModule
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CreatureCreationWindow : Window
    {
        public CreatureCreationWindow()
        {
            InitializeComponent();
        }

        private void MoveCreatureWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) { this.DragMove(); }
        }

        private void MinimizeCreatureCreation(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseCreatureCreation(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
