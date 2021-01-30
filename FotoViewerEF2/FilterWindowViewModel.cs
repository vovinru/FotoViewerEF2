using ClassLibraryFotoEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoViewerEF2
{
    public class FilterWindowViewModel
    {
        #region fields

        public bool _allCities = true;
        public bool _notCities = true;
        public List<City> _selectedCities = new List<City>();

        public bool _allCountries = true;
        public bool _notCountries = true;
        public List<Country> _selectedCountries = new List<Country>();

        #endregion

        #region properties

        /// <summary>
        /// Контекст с описанием фото
        /// </summary>
        public FotoContext FotoContext
        {
            get;
            set;
        }

        /// <summary>
        /// Флаг о том что выбираем все города
        /// </summary>
        public bool AllCities
        {
            get
            {
                if (_selectedCities.Count > 0)
                    _allCities = false;
                return _allCities;
            }
            set
            {
                if (value)
                    _selectedCities.Clear();

                _allCities = value;
            }
        }

        /// <summary>
        /// Флаг о том что выбираем фото без городов
        /// </summary>
        public bool NotCities
        {
            get
            {
                return _notCities;
            }
            set
            {
                _notCities = value;
            }
        }

        /// <summary>
        /// Выбранные города
        /// </summary>
        public List<City> SelectedCities
        {
            get
            {
                return _selectedCities;
            }
            set
            {
                _selectedCities = value;
            }
        }


        /// <summary>
        /// описание настройки городов
        /// </summary>
        public string SelectedCitiesText
        {
            get
            {
                string ret = string.Empty;

                if (SelectedCities != null && SelectedCities.Count > 0)
                {
                    SelectedCities.ForEach(c => ret += c.Name + " ");
                }

                if (AllCities)
                    ret = "Показываем все города";
                if (NotCities)
                    ret += "Показываем фото без городов";

                return ret;
            }
        }

        /// <summary>
        /// Флаг о том что выбираем все города
        /// </summary>
        public bool AllCountries
        {
            get
            {
                if (_selectedCountries.Count > 0)
                    _allCountries = false;
                return _allCountries;
            }
            set
            {
                if (value)
                    _selectedCountries.Clear();

                _allCountries = value;
            }
        }

        /// <summary>
        /// Флаг о том что выбираем фото без городов
        /// </summary>
        public bool NotCountries
        {
            get
            {
                return _notCountries;
            }
            set
            {
                _notCountries = value;
            }
        }

        /// <summary>
        /// Выбранные города
        /// </summary>
        public List<Country> SelectedCountries
        {
            get
            {
                return _selectedCountries;
            }
            set
            {
                _selectedCountries = value;
            }
        }


        /// <summary>
        /// описание настройки городов
        /// </summary>
        public string SelectedCountriesText
        {
            get
            {
                string ret = string.Empty;

                if (SelectedCountries != null && SelectedCountries.Count > 0)
                {
                    SelectedCountries.ForEach(c => ret += c.Name + " ");
                }

                if (AllCountries)
                    ret = "Показываем все города";
                if (NotCountries)
                    ret += "Показываем фото без городов";

                return ret;
            }
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
        /// Строка для дат
        /// </summary>
        public string SelectedDatesText
        {
            get
            {
                if (AllDates)
                    return "Выбираем любые даты";
                else
                    return string.Format("от {0} до {1}", DateStart, DateEnd);
            }
        }

        #endregion

        #region constructors

        public FilterWindowViewModel(FotoContext fotoContext)
        {
            FotoContext = fotoContext;
            AllDates = true;

            DateStart = DateTime.Now;
            DateEnd = DateTime.Now;

        }

        /// <summary>
        /// Получить фильтр
        /// </summary>
        /// <returns></returns>
        public Filter GetFilter()
        {
            Filter filter = new Filter();

            filter.AllCities = AllCities;
            filter.NotCities = NotCities;
            filter.SelectedCities = SelectedCities;

            filter.AllCountries = AllCountries;
            filter.NotCountries = NotCountries;
            filter.SelectedCountries = SelectedCountries;

            filter.AllDates = AllDates;
            filter.DateStart = DateStart;
            filter.DateEnd = DateEnd;

            return filter;
        }

        #endregion

    }
}
