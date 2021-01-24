using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoViewerEF2
{
    public class CheckListWindowViewModel
    {
        #region propertyes

        public List<object> Items
        {
            get;
            set;
        }

        #endregion

        #region constructors

        public CheckListWindowViewModel(List<object> items)
        {
            Items = items;
        }

        #endregion

    }
}
