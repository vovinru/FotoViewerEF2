using ClassLibraryFotoEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoViewerEF2
{
    /// <summary>
    /// Класс базовой ViewModel для фото работы
    /// </summary>
    public class BaseViewModel
    {
        /// <summary>
        /// Контекст фото БД
        /// </summary>
        public FotoContext FotoContext
        {
            get;
            set;
        }

        public BaseViewModel(FotoContext fotoContext)
        {
            FotoContext = fotoContext;
        }
    }
}
