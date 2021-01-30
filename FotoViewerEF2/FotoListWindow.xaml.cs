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
    /// Логика взаимодействия для FotoListWindow.xaml
    /// </summary>
    public partial class FotoListWindow : Window
    {
        FotoListWindowViewModel _viewModel;

        public FotoListWindow(FotoContext fotoContext, List<Foto> fotos, Foto selectFoto)
        {
            InitializeComponent();

            _viewModel = new FotoListWindowViewModel(fotoContext, fotos, selectFoto);
            UpdateViewModel();
        }

        public void UpdateViewModel()
        {
            DataContext = null;
            _viewModel.FotoContext.SaveChanges();
            DataContext = _viewModel;
        }

        public void LeftClick()
        {
            _viewModel.ShiftLeft();
            UpdateViewModel();
        }

        public void RightClick()
        {
            _viewModel.ShiftRight();
            UpdateViewModel();
        }

        private void buttonLeft_Click(object sender, RoutedEventArgs e)
        {
            LeftClick();
        }

        private void buttonRight_Click(object sender, RoutedEventArgs e)
        {
            RightClick();
        }


        private void Save100_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveTop100Foto();
            
        }

        private void ButtonDeleteCity_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SelectCity = null;
            UpdateViewModel();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                LeftClick();
            }
            if (e.Key == Key.Right)
            {
                RightClick();
            }
        }
    }
}
