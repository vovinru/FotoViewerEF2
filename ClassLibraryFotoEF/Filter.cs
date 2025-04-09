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
        /// Выбранные страны
        /// </summary>
        public List<Country> SelectedCountries
        {
            get;
            set;
        }

        /// <summary>
        /// Флаг о том что выбираем все города
        /// </summary>
        public bool AllPersons
        {
            get;
            set;
        }

        /// <summary>
        /// Флаг о том что выбираем фото без персонажей
        /// </summary>
        public bool NotPersons
        {
            get;
            set;
        }

        /// <summary>
        /// Выбранные персонажи
        /// </summary>
        public List<Person> SelectedPersons
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

        /// <summary>
        /// включен ли фильтр по имени
        /// </summary>
        public bool IsNameFilter
        {
            get;
            set;
        }

        /// <summary>
        /// значение фильтра по имени
        /// </summary>
        public string StringNameFilter
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

            AllPersons = true;
            NotPersons = true;
            SelectedPersons = new List<Person>();

            AllDates = true;
            DateStart = new DateTime();
            DateEnd = new DateTime();

            IsNameFilter = false;
            StringNameFilter = string.Empty;
        }

        #endregion
    }
}
