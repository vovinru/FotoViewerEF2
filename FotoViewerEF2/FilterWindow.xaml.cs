using ClassLibraryFotoEF;
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
    /// Логика взаимодействия для WindowFilter.xaml
    /// </summary>
    public partial class WindowFilter : Window
    {
        FilterWindowViewModel _viewModel;

        public WindowFilter(FotoContext fotoContext)
        {
            InitializeComponent();
            _viewModel = new FilterWindowViewModel(fotoContext);
            DataContext = _viewModel;
        }

        public void UpdateViewModel()
        {
            DataContext = null;
            DataContext = _viewModel;
        }

        public Filter GetFilter()
        {
            return _viewModel.GetFilter();
        }

        private void buttonChoiceCities_Click(object sender, RoutedEventArgs e)
        {
            CheckListWindow window = new CheckListWindow(_viewModel.FotoContext.Cities.ToList<object>());
            if(window.ShowDialog() == true)
            {
                _viewModel.SelectedCities = window.GetSelectedItems().Cast<City>().ToList();
                UpdateViewModel();
            }
        }

        private void buttonChoiceCountries_Click(object sender, RoutedEventArgs e)
        {
            CheckListWindow window = new CheckListWindow(_viewModel.FotoContext.Countries.ToList<object>());
            if (window.ShowDialog() == true)
            {
                _viewModel.SelectedCountries = window.GetSelectedItems().Cast<Country>().ToList();
                UpdateViewModel();
            }
        }

        private void buttonShow_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

    }
}
