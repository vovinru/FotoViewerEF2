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
    /// Логика взаимодействия для ListWindow.xaml
    /// </summary>
    public partial class ListWindow : Window
    {
        ListWindowViewModel _viewModel;

        public ListWindow(FotoContext fotoContext, FotoListType fotoListType)
        {
            InitializeComponent();

            _viewModel = new ListWindowViewModel(fotoContext, fotoListType);
            UpdateViewModel();
        }

        public void UpdateViewModel()
        {
            DataContext = null;
            _viewModel.UpdateItems();
            DataContext = _viewModel;
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            switch(_viewModel.FotoListType)
            {
                case FotoListType.Country:
                    {
                        CommonCommands.AddNewCountry(_viewModel.FotoContext);
                        break;
                    }

                case FotoListType.City:
                    {
                        City city = new City();
                        city.Name = "Новый город";
                        _viewModel.FotoContext.AddCity(city);

                        CityWindow window = new CityWindow(_viewModel.FotoContext, city);
                        window.ShowDialog();
                        break;
                    }
            }

            _viewModel.FotoContext.SaveChanges();
            UpdateViewModel();
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            if(_viewModel.SelectedItem == null)
            {
                MessageBox.Show("Не выбран элемент для редактирования!");
                return;
            }

            switch (_viewModel.FotoListType)
            {
                case FotoListType.Country:
                    {
                        Country country = (Country)_viewModel.SelectedItem;

                        CountryWindow window = new CountryWindow(_viewModel.FotoContext, country);
                        window.ShowDialog();
                        break;
                    }

                case FotoListType.City:
                    {
                        City city = (City)_viewModel.SelectedItem;

                        CityWindow window = new CityWindow(_viewModel.FotoContext, city);
                        window.ShowDialog();
                        break;

                    }
            }

            _viewModel.FotoContext.SaveChanges();
            UpdateViewModel();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedItem == null)
            {
                MessageBox.Show("Не выбран элемент для удаления!");
                return;
            }

            switch (_viewModel.FotoListType)
            {
                case FotoListType.Country:
                    {
                        Country country = (Country)_viewModel.SelectedItem;

                        if (MessageBox.Show(string.Format("Вы действительно хотите удалить страну: {0}?", country), "", MessageBoxButton.YesNo)
                            == MessageBoxResult.Yes)
                        {
                            _viewModel.FotoContext.Countries.Remove(country);
                            break;
                        }
                        break;
                    }

                case FotoListType.City:
                    {
                        City city = (City)_viewModel.SelectedItem;

                        if (MessageBox.Show(string.Format("Вы действительно хотите удалить город: {0}?", city), "", MessageBoxButton.YesNo)
                            == MessageBoxResult.Yes)
                        {
                            _viewModel.FotoContext.Cities.Remove(city);
                            break;
                        }
                        break;
                    }
            }

            _viewModel.FotoContext.SaveChanges();
            UpdateViewModel();
        }
    }
}
