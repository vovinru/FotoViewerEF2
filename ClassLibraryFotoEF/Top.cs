using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryFotoEF
{
    /// <summary>
    /// Промежуточное сохранение рейтинга лучших фотографий
    /// </summary>
    public class Top
    {
        public int TopId
        {
            get;
            set;
        }

        /// <summary>
        /// Строковый ключ для топа. (Общий, за год итп)
        /// </summary>
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// Дата топа
        /// </summary>
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// Описание топа
        /// </summary>
        public string Description
        {
            get;
            set;
        }
    }
}
