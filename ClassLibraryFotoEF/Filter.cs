using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryFotoEF
{
    /// <summary>
    /// Класс который настраивает фильтры для фото
    /// </summary>
    public class Filter
    {
        #region properties

        /// <summary>
        /// Флаг о том что выбираем все города
        /// </summary>
        public bool AllCities
        {
            get;
            set;
        }

        /// <summary>
        /// Флаг о том что выбираем фото без городов
        /// </summary>
        public bool NotCities
        {
            get;
            set;
        }

        /// <summary>
        /// Выбранные города
        /// </summary>
        public List<City> SelectedCities
        {
            get;
            set;
        }



        /// <summary>
        /// Флаг о том что выбираем все города
        /// </summary>
        public bool AllCountries
        {
            get;
            set;
        }

        /// <summary>
        /// Флаг о том что выбираем фото без городов
        /// </summary>
        public bool NotCountries
        {
            get;
            set;
        }

        /// <summary>
        /// Выбранные города
        /// </summary>
        public List<Country> SelectedCountries
        {
            get;
            set;
        }

        /// <summary>
        /// Любые даты
        /// </summary>
        public bool AllDates
        {
            get;
            set;
        }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime DateStart
        {
            get;
            set;
        }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime DateEnd
        {
            get;
            set;
        }

        #endregion

        #region constructors

        public Filter()
        {
            AllCities = true;
            NotCities = true;
            SelectedCities = new List<City>();

            AllCountries = true;
            NotCountries = true;
            SelectedCountries = new List<Country>();

            AllDates = true;
            DateStart = new DateTime();
            DateEnd = new DateTime();
        }

        #endregion
    }
}
