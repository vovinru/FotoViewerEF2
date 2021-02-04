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
    public partial class ListWindow : BaseWindow
    {

        public ListWindowViewModel ViewModel
        {
            get
            {
                return (ListWindowViewModel)_viewModel;
            }
        }

        public ListWindow(FotoContext fotoContext, FotoListType fotoListType)
        {
            InitializeComponent();

            _viewModel = new ListWindowViewModel(fotoContext, fotoListType);
            UpdateViewModel();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            switch(ViewModel.FotoListType)
            {
                case FotoListType.Country:
                    {
                        CommonCommands.AddNewCountry(_viewModel.FotoContext);
                        break;
                    }

                case FotoListType.City:
                    {
                        CommonCommands.AddNewCity(_viewModel.FotoContext);
                        break;
                    }

                case FotoListType.Person:
                    {
                        CommonCommands.AddNewPerson(_viewModel.FotoContext);
                        break;
                    }
            }

            _viewModel.FotoContext.SaveChanges();
            UpdateViewModel();
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            if(ViewModel.SelectedItem == null)
            {
                MessageBox.Show("Не выбран элемент для редактирования!");
                return;
            }

            switch (ViewModel.FotoListType)
            {
                case FotoListType.Country:
                    {
                        Country country = (Country)ViewModel.SelectedItem;

                        CountryWindow window = new CountryWindow(_viewModel.FotoContext, country);
                        window.ShowDialog();
                        break;
                    }

                case FotoListType.City:
                    {
                        City city = (City)ViewModel.SelectedItem;

                        CityWindow window = new CityWindow(_viewModel.FotoContext, city);
                        window.ShowDialog();
                        break;

                    }

                case FotoListType.Person:
                    {
                        Person person = (Person)ViewModel.SelectedItem;

                        PersonWindow window = new PersonWindow(_viewModel.FotoContext, person);
                        window.ShowDialog();
                        break;
                    }
            }

            _viewModel.FotoContext.SaveChanges();
            UpdateViewModel();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedItem == null)
            {
                MessageBox.Show("Не выбран элемент для удаления!");
                return;
            }

            switch (ViewModel.FotoListType)
            {
                case FotoListType.Country:
                    {
                        Country country = (Country)ViewModel.SelectedItem;

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
                        City city = (City)ViewModel.SelectedItem;

                        if (MessageBox.Show(string.Format("Вы действительно хотите удалить город: {0}?", city), "", MessageBoxButton.YesNo)
                            == MessageBoxResult.Yes)
                        {
                            _viewModel.FotoContext.Cities.Remove(city);
                            break;
                        }
                        break;
                    }

                case FotoListType.Person:
                    {
                        Person person = (Person)ViewModel.SelectedItem;

                        if (MessageBox.Show(string.Format("Вы действительно хотите удалить персонажа: {0}?", person), "", MessageBoxButton.YesNo)
                            == MessageBoxResult.Yes)
                        {
                            _viewModel.FotoContext.Persons.Remove(person);
                            break;
                        }
                        break;
                    }
            }

            _viewModel.FotoContext.SaveChanges();
            UpdateViewModel();
        }



        public override void UpdateViewModel()
        {
            ViewModel.UpdateItems();
            base.UpdateViewModel();
        }
    }
}
