using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ColorChooserX2.Util;
using ColorChooserX2.ViewModels;
using ColorChooserX2.Themes;

namespace ColorChooserX2.Views
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class ChooserX2 : UserControl
    {
        public ChooserX2()
        {
            InitializeComponent();
            DataContext = new HSVFieldViewModel() { /*ImageReader=new ImageReader(new UIElement[] { rgbRect, saturationRect }, saturationRect)*/};
        }

        /// <summary>
        /// Theme Dependency Property
        /// </summary>
        public static readonly DependencyProperty ThemeProperty =
            DependencyProperty.Register("Theme", typeof(Theme), typeof(ChooserX2),
                new FrameworkPropertyMetadata(null,
                    new PropertyChangedCallback(OnThemeChanged)));

        /// <summary>
        /// Gets or sets the Theme property.  
        /// </summary>
        public Theme Theme
        {
            get { return (Theme)GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }

        /// <summary>
        /// Handles changes to the Theme property.
        /// </summary>
        private static void OnThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ChooserX2)d).OnThemeChanged(e);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the Theme property.
        /// </summary>
        protected virtual void OnThemeChanged(DependencyPropertyChangedEventArgs e)
        {
            var oldTheme = e.OldValue as Theme;
            var newTheme = e.NewValue as Theme;

            if (oldTheme != null)
            {
                var resourceDictionaryToRemove =
                    Application.Current.Resources.MergedDictionaries.FirstOrDefault(r => r.Source == oldTheme.GetResourceUri());
                if (resourceDictionaryToRemove != null)
                    Application.Current.Resources.MergedDictionaries.Remove(
                        resourceDictionaryToRemove);
            }

            if (newTheme != null)
            {
                Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = newTheme.GetResourceUri() });
            }
        }

        
    }
}
