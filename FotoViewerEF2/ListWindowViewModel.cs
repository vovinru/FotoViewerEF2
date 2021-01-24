using ClassLibraryFotoEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoViewerEF2
{
    public class ListWindowViewModel
    {
        #region fields
        #endregion

        #region properties
        
        /// <summary>
        /// Заголовок списка
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Объекты
        /// </summary>
        public List<Object> Items
        {
            get;
            set;
        }

        /// <summary>
        /// Выбранный объект
        /// </summary>
        public Object SelectedItem
        {
            get;
            set;
        }

        /// <summary>
        /// Ссылка на контейнер с фото и остальным
        /// </summary>
        public FotoContext FotoContext
        {
            get;
            set;
        }

        /// <summary>
        /// Тип объекта который в списке
        /// </summary>
        public FotoListType FotoListType
        {
            get;
            set;
        }

        #endregion

        #region constructors

        public ListWindowViewModel(FotoContext fotoContext, FotoListType fotoListType)
        {
            FotoContext = fotoContext;
            FotoListType = fotoListType;

            UpdateItems();
        }

        #endregion

        #region methods

        public void UpdateItems()
        {
            SelectedItem = null;
            Items = new List<object>();

            switch(FotoListType)
            {
                case FotoListType.City:
                    foreach (Object obj in FotoContext.Cities)
                        Items.Add(obj);
                    break;

                case FotoListType.Country:
                    foreach (Object obj in FotoContext.Countries)
                        Items.Add(obj);
                    break;
            }
        }

        #endregion
    }
}
