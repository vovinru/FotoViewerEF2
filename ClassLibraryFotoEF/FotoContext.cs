using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryFotoEF
{
    public class FotoContext:DbContext
    {

        public DbSet<Foto> Fotos
        {
            get;
            set;
        }

        public DbSet<City> Cities
        {
            get;
            set;
        }

        public DbSet<Country> Countries
        {
            get;
            set;
        }

        public FotoContext()
            :base("DBConnection_Test")
        {
            Cities.Load();
        }

        public Foto GetRandomFoto()
        {
            if (Fotos.Count() > 1)
            {
                return Fotos.OrderBy(f => Guid.NewGuid()).Take(1).First();
            }

            return null;
        }

        public bool ContainFoto(string filename)
        {
            return Fotos.Any(f => f.FileName == filename);
        }

        public void AddFoto(string filename)
        {
            Foto foto = new Foto();
            foto.FileName = filename;

            Fotos.Add(foto);
        }

        public void AddCountry(Country country)
        {
            Countries.Add(country);
        }

        public void AddCity(City city)
        {
            Cities.Add(city);
        }

        public City GetCity(int cityId)
        {
            return Cities.FirstOrDefault(c => c.CityId == cityId);
        }

        public List<Foto> GetFotosByFilter(Filter filter)
        {
            List<Foto> fotos = Fotos.ToList();

            if (!filter.NotCities)
                fotos.RemoveAll(f => f.CityId == null);
            if (!filter.AllCities)
                fotos.RemoveAll(f => f.CityId != null && !filter.SelectedCities.Any(c => c.CityId == f.CityId));


            if (!filter.NotCountries)
                fotos.RemoveAll(f => f.CityId == null);
            if (!filter.AllCities)
            {
                for(int i=0; i<fotos.Count;i++)
                {
                    if (fotos[i].CityId == null)
                        continue;

                    Country country = GetCity(fotos[i].CityId.Value).Country;

                    if(!filter.SelectedCountries.Contains(country))
                    {
                        fotos.Remove(fotos[i]);
                        i--;
                    }
                }
            }

            if(!filter.AllDates)
            {
                for (int i = 0; i < fotos.Count; i++)
                {
                    if(fotos[i].Date <filter.DateStart)
                    {
                        fotos.Remove(fotos[i]);
                        i--;
                        continue;
                    }

                    if (fotos[i].Date > filter.DateEnd)
                    {
                        fotos.Remove(fotos[i]);
                        i--;
                        continue;
                    }
                }
            }

            fotos.Sort(Foto.EqualsByFileDate);

            return fotos;
        }

    }
}
