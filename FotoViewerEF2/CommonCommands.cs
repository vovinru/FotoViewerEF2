using ClassLibraryFotoEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoViewerEF2
{
    public static class CommonCommands
    {
        public static void AddNewCountry(FotoContext fotoContext)
        {
            Country country = new Country();
            country.Name = "Новая страна";
            fotoContext.AddCountry(country);

            CountryWindow window = new CountryWindow(fotoContext, country);
            window.ShowDialog();

            fotoContext.SaveChanges();
        }

        public static void AddNewCity(FotoContext fotoContext)
        {
            City city = new City();
            city.Name = "Новый город";
            fotoContext.AddCity(city);

            CityWindow window = new CityWindow(fotoContext, city);
            window.ShowDialog();
        }

        public static void AddNewPerson(FotoContext fotoContext)
        {
            Person person = new Person();
            person.Name = "Новый персонаж";
            fotoContext.AddPerson(person);

            PersonWindow window = new PersonWindow(fotoContext, person);
            window.ShowDialog();
        }
    }
}
