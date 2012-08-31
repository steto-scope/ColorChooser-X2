using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using ColorChooserX2.ViewModels;

namespace ColorChooserX2.Views
{
    class ChooserX2TemplateSelector : DataTemplateSelector
    {
        public DataTemplate RGBFieldTemplate { get; set; }

        public DataTemplate Selected { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is HSVFieldViewModel)
            {
                Selected = RGBFieldTemplate;
                return RGBFieldTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
