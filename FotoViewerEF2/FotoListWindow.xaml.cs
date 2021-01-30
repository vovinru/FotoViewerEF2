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
    public partial class FotoListWindow : BaseWindow
    {
        public FotoListWindowViewModel ViewModel
        {
            get
            {
                return (FotoListWindowViewModel)_viewModel;
            }
        }

        public FotoListWindow(FotoContext fotoContext, List<Foto> fotos, Foto selectFoto)
        {
            InitializeComponent();

            _viewModel = new FotoListWindowViewModel(fotoContext, fotos, selectFoto);
            UpdateViewModel();
        }

        public void LeftClick()
        {
            ViewModel.ShiftLeft();
            UpdateViewModel();
        }

        public void RightClick()
        {
            ViewModel.ShiftRight();
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
            ViewModel.SaveTop100Foto();
            
        }

        private void ButtonDeleteCity_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectCity = null;
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
