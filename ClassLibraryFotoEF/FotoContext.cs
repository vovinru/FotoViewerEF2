using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
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

        public DbSet<Person> Persons
        {
            get;
            set;
        }

        public FotoContext()
            //:base("DBConnection_Test")
            :base("DBConnection_2")
        {

            Fotos.Load();
            Cities.Load();
            Countries.Load();
            Persons.Load();

            Fotos.Include(f => f.Persons).ToList();
            Persons.Include(p => p.Fotoes).ToList();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notGame"></param>
        /// <param name="count">количество фото (если notGame = true) то всегда 1</param>
        /// <returns></returns>
        public List<Foto> GetRandomFotos(bool notGame = false, int count = 1)
        {
            if (Fotos.Count() > 1)
            {
                if(notGame)
                {
                    return new List<Foto> { Fotos.OrderBy(f => Guid.NewGuid()).First(f => f.CountLose + f.CountWin == 0) };
                }    
                return Fotos.OrderBy(f => Guid.NewGuid()).Take(count).ToList();
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

        public void AddPerson(Person person)
        {
            Persons.Add(person);
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
            if (!filter.AllCountries)
            {
                for(int i=0; i<fotos.Count;i++)
                {
                    if (fotos[i].City == null)
                        continue;

                    Country country = fotos[i].City.Country;

                    if(!filter.SelectedCountries.Contains(country))
                    {
                        fotos.Remove(fotos[i]);
                        i--;
                    }
                }
            }

            if (!filter.NotPersons)
                fotos.RemoveAll(f => f.Persons.Count == 0 && !f.NotPersons);
            if (!filter.AllPersons)
                fotos.RemoveAll(f => (!filter.SelectedPersons.Any(p => f.Persons.Contains(p))) &&
                                        !(f.Persons.Count==0 && !f.NotPersons));

            if (filter.IsNameFilter)
            {
                fotos.RemoveAll(f => !f.FileName.Contains(filter.StringNameFilter));
            }

            if (!filter.AllDates)
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

            for(int i=0; i<fotos.Count;i++)
            {
                if(!File.Exists(fotos[i].FileName))
                {
                    DeleteFoto(fotos[i]);
                    fotos.Remove(fotos[i]);
                    i--;
                }
            }

            fotos.Sort(Foto.EqualsByFileName);

            return fotos;
        }

        /// <summary>
        /// Получить все персоны
        /// </summary>
        /// <returns></returns>
        public List<Person> GetPersons()
        {
            return Persons.ToList();
        }

        //Удаляем фото из бд
        public void DeleteFoto(Foto foto)
        {
            if (foto.City != null)
                foto.City.Fotoes.Remove(foto);
            foto.Persons.All(p => p.Fotoes.Remove(foto));

            Fotos.Remove(foto);
        }

        /// <summary>
        /// Получить текущий базовый штраф
        /// </summary>
        /// <returns></returns>
        public int GetBasePenalty(int countFoto0Penalty)
        {
            if (countFoto0Penalty < 512)
                return 0;

            else
                return (countFoto0Penalty - 512) / 25;
        }

        public PenaltyReport GetPenaltyReport(Filter filter = null)
        {
            if(filter == null)
                filter = new Filter(); 

            PenaltyReport penaltyReport = new PenaltyReport();

            List<Foto> fotos = GetFotosByFilter(filter);

            foreach(Foto foto in fotos)
            {
                penaltyReport.AddPenalty(foto.CountPenalty);
            }

            penaltyReport.CalculationResult();

            return penaltyReport;
        }

    }
}
