﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Kanch.ProfileComponents.ViewModels;

namespace Kanch.ProfileComponents.Views
{
    /// <summary>
    /// Interaction logic for TripsManagementOfAdminView.xaml
    /// </summary>
    public partial class TripsManagementOfAdminView : UserControl
    {
        public TripsManagementOfAdminView()
        {
            this.DataContext = new TripsManagementOfAdminViewModel();
            InitializeComponent();
        }
    }
}
