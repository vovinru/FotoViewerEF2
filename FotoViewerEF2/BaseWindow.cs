using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FotoViewerEF2
{
    /// <summary>
    /// Логика взаимодействия для BaseWindow.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {
        public BaseViewModel _viewModel;

        public BaseWindow()
        {
        }

        public virtual void UpdateViewModel()
        {
            DataContext = null;
            _viewModel.FotoContext.SaveChanges();
            DataContext = _viewModel;
        }
    }
}
