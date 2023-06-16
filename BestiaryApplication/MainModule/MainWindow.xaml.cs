using BestiaryApplication.GamesModule;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BestiaryApplication.MainModule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Storyboard? showSlideMenuStoryboard;
        private Storyboard? hideSlideMenuStoryboard;

        public MainWindow()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            SetupAnimations();
        }

        private void SetupAnimations()
        {
            ThicknessAnimation showSlideMenuAnimation = new()
            {
                From = new Thickness(-1075, 0, 1075, 0),
                To = new Thickness(0, 0, 0, 0),
                Duration = new Duration(TimeSpan.FromSeconds(0.5)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTarget(showSlideMenuAnimation, slide_option_menu);
            Storyboard.SetTargetProperty(showSlideMenuAnimation, new PropertyPath(MarginProperty));

            showSlideMenuStoryboard = new();
            showSlideMenuStoryboard.Children.Add(showSlideMenuAnimation);

            ThicknessAnimation hideSlideMenuAnimation = new()
            {
                From = new Thickness(0, 0, 0, 0),
                To = new Thickness(-1075, 0, 1075, 0),
                Duration = new Duration(TimeSpan.FromSeconds(0.3)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };

            Storyboard.SetTarget(hideSlideMenuAnimation, slide_option_menu);
            Storyboard.SetTargetProperty(hideSlideMenuAnimation, new PropertyPath(MarginProperty));

            hideSlideMenuStoryboard = new();
            hideSlideMenuStoryboard.Children.Add(hideSlideMenuAnimation);
        }

        private void ShowMenu(object sender, RoutedEventArgs e)
        {
            showSlideMenuStoryboard!.Begin();
        }

        private void HideSlideOptionMenu(object sender, RoutedEventArgs e)
        {
            hideSlideMenuStoryboard!.Begin();
        }

        private void MoveMainWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) { this.DragMove(); }
        }

        private void MinimizeApplication(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseApplication(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void AddNewCreature(object sender, RoutedEventArgs e)
        {
            CreatureCreationWindow creatureCreationWindow = new();
            creatureCreationWindow.ShowDialog();
        }
    }
}
