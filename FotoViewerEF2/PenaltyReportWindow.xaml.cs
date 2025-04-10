﻿using ClassLibraryFotoEF;
using System;
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
using System.Windows.Shapes;

namespace FotoViewerEF2
{
    /// <summary>
    /// Interaction logic for PenaltyReportWindow.xaml
    /// </summary>
    public partial class PenaltyReportWindow : Window
    {
        public PenaltyReportWindow(PenaltyReport report)
        {
            InitializeComponent();
            DataContext = new PenaltyReportViewModel(report);
        }
    }
}
