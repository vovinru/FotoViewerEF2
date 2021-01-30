using ClassLibraryFotoEF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoViewerEF2
{
    public class FotoListWindowViewModel:BaseViewModel
    {

        List<Foto> Fotos
        {
            get;
            set;
        }

        Foto SelectFoto
        {
            get;
            set;
        }

        public string FotoFileName
        {
            get
            {
                return SelectFoto.FileName;
            }
        }

        public string FotoMainStatistics
        {
            get
            {
                return SelectFoto.GetFotoInfo();
            }
        }

        public string FileName
        {
            get
            {
                return SelectFoto.FileName;
            }
        }

        public List<City> Cities
        {
            get;
            set;
        }

        public City SelectCity
        {
            get
            {
                if (SelectFoto.CityId.HasValue)
                    return FotoContext.GetCity(SelectFoto.CityId.Value);
                return null;
            }
            set
            {
                if (value == null)
                    SelectFoto.CityId = null;
                else
                    SelectFoto.CityId = value.CityId;
            }
        }

        public Country SelectCountry
        {
            get
            {
                if (SelectCity == null)
                    return null;

                return SelectCity.Country;
            }
        }

        public string NumberCurrent
        {
            get
            {
                return string.Format("{0}/{1}", Fotos.IndexOf(SelectFoto) + 1, Fotos.Count);
            }
        }

        public FotoListWindowViewModel(FotoContext fotoContext, List<Foto> fotos, Foto selectedFoto) :
            base(fotoContext)
        {
            fotos.ForEach(f => f.CalculateRaiting());

            SelectFoto = selectedFoto;

            Fotos = fotos;

            Cities = new List<City>();
            foreach (City city in FotoContext.Cities)
                Cities.Add(city);
        }

        public void ShiftLeft()
        {
            int indexCurrent = Fotos.IndexOf(SelectFoto);
            indexCurrent--;

            if (indexCurrent < 0)
                indexCurrent = Fotos.Count - 1;

            SelectFoto = Fotos[indexCurrent];
        }

        public void ShiftRight()
        {
            int indexCurrent = Fotos.IndexOf(SelectFoto);
            indexCurrent++;

            if (indexCurrent >= Fotos.Count)
                indexCurrent = 0;

            SelectFoto = Fotos[indexCurrent];
        }

        public void SaveTop100Foto()
        {
            List<FotoSaveTopJSON> fotosJson = new List<FotoSaveTopJSON>();

            for(int i=0; i<100; i++)
            {
                Foto foto = Fotos[i];
                File.Copy(foto.FileName, string.Format("TOP/{0}.jpg", i+1));

                FotoSaveTopJSON fotoJson = new FotoSaveTopJSON();
                fotoJson.Place = i + 1;
                fotoJson.FileName = foto.FileName;
                fotoJson.CountWin = foto.CountWin;
                fotoJson.CountLose = foto.CountLose;
                fotoJson.Raiting = foto.Raiting;

                fotosJson.Add(fotoJson);
            }

            //Сохранение в json
            string json = JsonConvert.SerializeObject(fotosJson);
            
            using(StreamWriter file = new StreamWriter("TOP/top.json"))
            {
                file.Write(json);
            }
        }

    }
}
