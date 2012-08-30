﻿using System;
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
            DataContext = new RGBFieldViewModel() { /*ImageReader=new ImageReader(new UIElement[] { rgbRect, saturationRect }, saturationRect)*/};
        }

        


        
    }
}
