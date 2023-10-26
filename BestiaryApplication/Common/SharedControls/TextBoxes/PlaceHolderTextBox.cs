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

namespace BestiaryApplication.Common.SharedControls.TextBoxes
{
    public class PlaceHolderTextBox : TextBox
    {
        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }

        public static readonly DependencyProperty PlaceHolderProperty =
            DependencyProperty.Register("PlaceHolder", typeof(string),
                                        typeof(PlaceHolderTextBox),
                                        new PropertyMetadata(string.Empty));

        public bool IsEmpty
        {
            get { return (bool)GetValue(IsEmptyProperty); }
            private set { SetValue(IsEmptyPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey IsEmptyPropertyKey =
            DependencyProperty.RegisterReadOnly("IsEmpty", typeof(bool),
                                        typeof(PlaceHolderTextBox),
                                        new PropertyMetadata(true));

        public static readonly DependencyProperty IsEmptyProperty =
                                                  IsEmptyPropertyKey.DependencyProperty;

        static PlaceHolderTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceHolderTextBox), new FrameworkPropertyMetadata(typeof(PlaceHolderTextBox)));
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            IsEmpty = string.IsNullOrEmpty(Text);
        }
    }
}
