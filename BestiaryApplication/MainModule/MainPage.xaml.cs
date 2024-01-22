using BestiaryApplication.Common.Resources.Assets.Cursors;
using BestiaryApplication.Common.utils;
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

namespace BestiaryApplication.MainModule
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnLatestCreatureImageMouseEnter(object sender, MouseEventArgs e)
        {
            last_consulted_creature_image.Cursor = CustomCursors.ZoomCursor;
        }

        private void OnLatestCreatureImageMouseDown(object sender, MouseButtonEventArgs e)
        {
            var image = (Image)sender;

            if (image.Source != null) GeneralMethods.ZoomImage(image.Source);
        }

        
    }
}
