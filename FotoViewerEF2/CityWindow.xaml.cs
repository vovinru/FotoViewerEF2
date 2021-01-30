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
    /// Логика взаимодействия для CityWindow.xaml
    /// </summary>
    public partial class CityWindow : BaseWindow
    {

        public CityWindow(FotoContext fotoContext, City city)
        {
            InitializeComponent();
            _viewModel = new CityWindowViewModel(fotoContext, city);
            UpdateViewModel();
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonCreateCountry_Click(object sender, RoutedEventArgs e)
        {
            CommonCommands.AddNewCountry(_viewModel.FotoContext);
            UpdateViewModel();
        }
    }
}
