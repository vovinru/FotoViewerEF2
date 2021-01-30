using ClassLibraryFotoEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoViewerEF2
{
    public class CheckListWindowViewModel:BaseViewModel
    {
        #region propertyes

        public List<object> Items
        {
            get;
            set;
        }

        #endregion

        #region constructors

        public CheckListWindowViewModel(FotoContext fotoContext, List<object> items) :
            base(fotoContext)
        {
            Items = items;
        }

        #endregion

    }
}
