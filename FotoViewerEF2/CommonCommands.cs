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

            CountryWindow window = new CountryWindow(country);
            window.ShowDialog();

            fotoContext.SaveChanges();
        }
    }
}
