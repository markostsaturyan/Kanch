﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kanch
{
    /// <summary>
    /// Interaction logic for AboutUs.xaml
    /// </summary>
    public partial class AboutUs : Page
    {
        private AboutUsViewModel viewModel;
        public AboutUs()
        {
            InitializeComponent();
            this.viewModel = new AboutUsViewModel();
        }

        private void ScrollChanged(object sender, ScrollEventArgs e)
        {
            ScrollBar sb = e.OriginalSource as ScrollBar;

            if (sb.Orientation == Orientation.Horizontal)
                return;
        }
    }
}