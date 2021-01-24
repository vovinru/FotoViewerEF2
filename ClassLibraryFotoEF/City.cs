using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryFotoEF
{
    public class City
    {
        public int CityId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int? CountryId
        {
            get;
            set;
        }
        public Country Country
        {
            get;
            set;
        }

        public ICollection<Foto> Fotoes
        {
            get;
            set;
        }


        public City()
        {
            Fotoes = new List<Foto>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
