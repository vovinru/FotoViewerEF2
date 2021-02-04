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
    /// Логика взаимодействия для PersonWindow.xaml
    /// </summary>
    public partial class PersonWindow : BaseWindow
    {
        public PersonWindow(FotoContext fotoContext, Person person)
        {
            InitializeComponent();
            _viewModel = new PersonWindowViewModel(fotoContext, person);
            UpdateViewModel();
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
