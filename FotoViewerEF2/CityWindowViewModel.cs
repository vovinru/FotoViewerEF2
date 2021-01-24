using ClassLibraryFotoEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoViewerEF2
{
    public class CityWindowViewModel
    {
        public FotoContext FotoContext
        {
            get;
            set;
        }

        public City City
        {
            get;
            set;
        }

        public string CityName
        {
            get
            {
                return City.Name;
            }
            set
            {
                City.Name = value;
            }
        }

        public List<Country> Countries
        {
            get
            {
                List <Country> ret = new List<Country>();
                foreach (Country country in FotoContext.Countries)
                    ret.Add(country);

                return ret;
            }
        }

        public Country SelectCountry
        {
            get
            {
                return City.Country;
            }
            set
            {
                City.Country = value;
            }
        }

        public CityWindowViewModel(FotoContext fotoContext, City city)
        {
            FotoContext = fotoContext;
            City = city;
        }
    }
}
