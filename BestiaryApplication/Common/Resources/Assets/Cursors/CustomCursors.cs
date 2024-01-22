using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BestiaryApplication.Common.Resources.Assets.Cursors
{
    class CustomCursors
    {
        private static readonly Cursor zoomCursor = new("C:\\Users\\ADRIAN\\source\\repos\\BestiaryApplication\\BestiaryApplication\\Common\\Resources\\Assets\\Cursors\\zoom_cursor.cur");

        public static Cursor ZoomCursor
        {
            get { return zoomCursor;  }
        }

    }
}
