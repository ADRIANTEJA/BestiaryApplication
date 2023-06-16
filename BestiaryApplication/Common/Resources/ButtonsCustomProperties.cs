using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BestiaryApplication.Common.Resources
{
    class ButtonsCustomProperties
    {
        public ButtonsCustomProperties() { }

        public Brush? MouseOverBackground { get; set; }
        public Brush? IsPressedBackground { get; set; }
        public double OnEventOpacity { get; set; }
        public double Opacity { get; set; }
    }
}
