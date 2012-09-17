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
using ColorChooserX2.ViewModels;
using ColorChooserX2.Themes;

namespace ColorChooserX2TestApp
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IColorChooser c = (IColorChooser)chooser.DataContext;
            c.SelectedColor = Color.FromArgb(128, 18, 200, 155);
            ((HSVFieldViewModel)c).EnableAlphaChannel = true;
            chooser.Theme = new DarkTheme();
        }
    }
}
