using ClassLibraryFotoEF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FotoViewerEF2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel();
            UpdateViewModel();
        }

        public void UpdateViewModel()
        {
            DataContext = null;
            DataContext = _viewModel;
        }

        private void buttonChoiceFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if(folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _viewModel.AddFotoFolder(folderDialog.SelectedPath);
                UpdateViewModel();
            }
        }

        private void buttonFoto1_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ClickFoto1();
            UpdateViewModel();
        }

        private void buttonFoto2_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ClickFoto2();
            UpdateViewModel();
        }

        private void menuItemCityList_Click(object sender, RoutedEventArgs e)
        {
            ListWindow window = new ListWindow(_viewModel.FotoContext, FotoListType.City);
            window.ShowDialog();
        }

        private void menuItemCountryList_Click(object sender, RoutedEventArgs e)
        {
            ListWindow window = new ListWindow(_viewModel.FotoContext, FotoListType.Country);
            window.ShowDialog();
        }

        private void menuItemManageLeftFoto_Click(object sender, RoutedEventArgs e)
        {
            FotoListWindow window = new FotoListWindow(_viewModel.FotoContext,
                new List<Foto> { _viewModel.Foto1 }, _viewModel.Foto1);

            window.ShowDialog();
            UpdateViewModel();
        }

        private void menuItemManageRightFoto_Click(object sender, RoutedEventArgs e)
        {
            FotoListWindow window = new FotoListWindow(_viewModel.FotoContext,
                new List<Foto> { _viewModel.Foto2 }, _viewModel.Foto2);

            window.ShowDialog();
            UpdateViewModel();
        }

        private void menuItemSortRaiting_Click(object sender, RoutedEventArgs e)
        {
            List<Foto> fotos = new List<Foto>();

            foreach (Foto foto in _viewModel.FotoContext.Fotos)
                fotos.Add(foto);


            fotos.ForEach(f => f.CalculateRaiting());
            fotos.Sort((f1, f2) => (int)(1000*(f2.Raiting - f1.Raiting)));

            FotoListWindow window = new FotoListWindow(_viewModel.FotoContext,
                fotos, fotos.First());

            window.ShowDialog();
            UpdateViewModel();
        }

        private void buttonRotateFoto1_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RotateFoto1();
            UpdateViewModel();
        }

        private void buttonRotateFoto2_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RotateFoto2();
            UpdateViewModel();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.Left)
            {
                _viewModel.ClickFoto1();
                UpdateViewModel();
            }
            if(e.Key == Key.Right)
            {
                _viewModel.ClickFoto2();
                UpdateViewModel();
            }
        }

        private void menuItemFilter_Click(object sender, RoutedEventArgs e)
        {
            WindowFilter window = new WindowFilter(_viewModel.FotoContext);

            if(window.ShowDialog() == true)
            {
                Filter filter = window.GetFilter();
                List<Foto> fotos = _viewModel.FotoContext.GetFotosByFilter(filter);

                FotoListWindow windowList = new FotoListWindow(_viewModel.FotoContext,
                fotos, fotos.First());

                windowList.ShowDialog();
                UpdateViewModel();

            }
        }
    }
}
