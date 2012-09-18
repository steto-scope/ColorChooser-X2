using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ColorChooserX2.Themes
{
     public abstract class Theme : DependencyObject
    {
        public Theme()
        {
            
        }

        public abstract Uri GetResourceUri();



        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(Theme), new UIPropertyMetadata(""));


    }
}
