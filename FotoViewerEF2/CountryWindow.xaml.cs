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
    /// Логика взаимодействия для CountryWindow.xaml
    /// </summary>
    public partial class CountryWindow : Window
    {
        CountryWindowViewModel _viewModel;

        public CountryWindow(Country country)
        {
            InitializeComponent();

            _viewModel = new CountryWindowViewModel(country);
            UpdateViewModel();
        }

        public void UpdateViewModel()
        {
            DataContext = null;
            DataContext = _viewModel;
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}