using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorChooserX2.Themes
{
    public class DarkTheme : Theme
    {
        public override Uri GetResourceUri()
        {
            return new Uri("/ColorChooserX2;component/Themes/Dark/Theme.xaml",UriKind.Relative);  
        }

        public override string ToString()
        {
            return "Dark";
        }

    }
}
