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
    /// Логика взаимодействия для CheckListWindow.xaml
    /// </summary>
    public partial class CheckListWindow : Window
    {
        CheckListWindowViewModel _viewModel;

        public CheckListWindow(List<object> items)
        {
            InitializeComponent();

            _viewModel = new CheckListWindowViewModel(items);
            DataContext = _viewModel;
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        /// <summary>
        /// Получить выделенные элементы
        /// </summary>
        /// <returns></returns>
        public List<object> GetSelectedItems()
        {
            List<object> ret = new List<object>();

            for (int i = 0; i < listBoxThis.SelectedItems.Count; i++)
            {
                ret.Add(listBoxThis.SelectedItems[i]);
            }

            return ret;
        }
    }
}
