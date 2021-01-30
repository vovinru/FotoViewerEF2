using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryFotoEF
{
    public class Country
    {
        public int CountryId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public ICollection<City> Cities
        {
            get;
            set;
        }

        public Country()
        {
            Cities = new List<City>();
        }

        public override string ToString()
        {
            return String.Format("{0} ({1})", Name, Cities.Sum(c => c.Fotoes.Count));
        }
    }
}
