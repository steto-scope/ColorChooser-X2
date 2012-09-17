using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorChooserX2.Themes
{
    public class DefaultTheme : Theme
    {
        public override Uri GetResourceUri()
        {
            return new Uri("/ColorChooserX2;component/Themes/Default/Theme.xaml",UriKind.Relative);  
        }
    }
}
