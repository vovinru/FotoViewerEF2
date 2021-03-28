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
    public partial class WindowFilter : BaseWindow
    {
        public FilterWindowViewModel ViewModel
        {
            get
            {
                return (FilterWindowViewModel)_viewModel;
            }
        }


        public WindowFilter(FotoContext fotoContext)
        {
            InitializeComponent();
            _viewModel = new FilterWindowViewModel(fotoContext);
            DataContext = _viewModel;
        }

        public Filter GetFilter()
        {
            return ViewModel.GetFilter();
        }

        private void buttonChoiceCities_Click(object sender, RoutedEventArgs e)
        {
            CheckListWindow window = new CheckListWindow(_viewModel.FotoContext, _viewModel.FotoContext.Cities.ToList<object>());
            if(window.ShowDialog() == true)
            {
                ViewModel.SelectedCities = window.GetSelectedItems().Cast<City>().ToList();
                UpdateViewModel();
            }
        }

        private void buttonChoiceCountries_Click(object sender, RoutedEventArgs e)
        {
            CheckListWindow window = new CheckListWindow(_viewModel.FotoContext, _viewModel.FotoContext.Countries.ToList<object>());
            if (window.ShowDialog() == true)
            {
                ViewModel.SelectedCountries = window.GetSelectedItems().Cast<Country>().ToList();
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

        private void buttonChoicePersons_Click(object sender, RoutedEventArgs e)
        {
            CheckListWindow window = new CheckListWindow(_viewModel.FotoContext, _viewModel.FotoContext.Persons.ToList<object>());
            if (window.ShowDialog() == true)
            {
                ViewModel.SelectedPersons = window.GetSelectedItems().Cast<Person>().ToList();
                UpdateViewModel();
            }
        }
    }
}
