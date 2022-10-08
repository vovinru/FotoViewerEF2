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
    public class FotoListWindowViewModel : BaseViewModel
    {
        //константы для категорий тегов
        public const string PERSON_TAG_STRING = "Персонажи";
        //---

        #region propertyes

        List<Foto> Fotos
        {
            get;
            set;
        }

        /// <summary>
        /// Группа фото (для изменения большого количества фотографий)
        /// </summary>
        List<Foto> GroupFotos
        {
            get;
            set;
        }

        /// <summary>
        /// Идет ли создание группы
        /// </summary>
        public bool CreateGroup
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
                return SelectFoto.City;
            }
            set
            {
                SelectFoto.City = value;

                foreach (Foto f in GroupFotos)
                {
                    f.City = value;
                }
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

        public int FotoAngle
        {
            get
            {
                return SelectFoto.Rotate;
            }
        }

        #region tags_propertyes

        /// <summary>
        /// Категория тегов которая выбрана в форме для редактирования
        /// </summary>
        public string SelectedCategory
        {
            get;
            set;
        }

        /// <summary>
        /// Все категории тегов которые существуют
        /// </summary>
        public List<string> Categorys
        {
            get;
            set;
        }

        /// <summary>
        /// Выбираем что по выбранной категории ничего нет (например на фото нет персонажей)
        /// </summary>
        public bool NotTag
        {
            get
            {
                if (SelectedCategory == PERSON_TAG_STRING)
                    return SelectFoto.NotPersons;

                return false;
            }
            set
            {
                if (SelectedCategory == PERSON_TAG_STRING)
                    SelectFoto.NotPersons = value;
            }
        }

        /// <summary>
        /// Теги выбранной категории для выбранного фото
        /// </summary>
        public List<object> FotoTags
        {
            get
            {
                if (SelectedCategory == PERSON_TAG_STRING)
                {
                    if (SelectFoto.Persons != null)
                        return SelectFoto.Persons.Cast<object>().ToList();
                }

                return new List<object>();
            }
        }

        /// <summary>
        /// Выделенный тег выбранной категории
        /// </summary>
        public object SelectFotoTag
        {
            get;
            set;
        }

        /// <summary>
        /// Все варианты тегов для данной категории
        /// </summary>
        public List<object> AllTags
        {
            get
            {
                if (SelectedCategory == PERSON_TAG_STRING)
                {
                    if (FotoContext.Persons != null)
                        return FotoContext.GetPersons().Cast<object>().ToList();
                }

                return new List<object>();
            }
        }

        /// <summary>
        /// Выденный тег, из всех моделей
        /// </summary>
        public object SelectOtherTag
        {
            get;
            set;
        }

        #endregion

        #endregion

        public FotoListWindowViewModel(FotoContext fotoContext, List<Foto> fotos, Foto selectedFoto) :
            base(fotoContext)
        {
            fotos.ForEach(f => f.CalculateRaiting());

            SelectFoto = selectedFoto;

            Fotos = fotos;

            Cities = new List<City>();
            foreach (City city in FotoContext.Cities)
                Cities.Add(city);

            GroupFotos = new List<Foto>();
            CreateGroup = false;

            ///Заполняем теги

            Categorys = new List<string> { PERSON_TAG_STRING };
            SelectedCategory = PERSON_TAG_STRING;

            ///--

        }

        #region methods

        public void ShiftLeft()
        {
            int indexCurrent = Fotos.IndexOf(SelectFoto);
            indexCurrent--;

            if (indexCurrent < 0)
                indexCurrent = Fotos.Count - 1;

            if (CreateGroup)
                GroupFotos.Remove(SelectFoto);
            else
                GroupFotos.Clear();

            SelectFoto = Fotos[indexCurrent];
        }

        public void ShiftRight()
        {
            int indexCurrent = Fotos.IndexOf(SelectFoto);
            indexCurrent++;

            if (indexCurrent >= Fotos.Count)
                indexCurrent = 0;

            if (CreateGroup)
                GroupFotos.Add(SelectFoto);
            else
                GroupFotos.Clear();

            SelectFoto = Fotos[indexCurrent];
        }

        public void SaveTop100Foto()
        {
            List<FotoSaveTopJSON> fotosJson = new List<FotoSaveTopJSON>();

            for (int i = 0; i < 100; i++)
            {
                Foto foto = Fotos[i];
                File.Copy(foto.FileName, string.Format("TOP/{0}.jpg", i + 1));

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

            using (StreamWriter file = new StreamWriter("TOP/top.json"))
            {
                file.Write(json);
            }
        }

        #region tags methods

        /// <summary>
        /// Добавляем тег к фотографии
        /// </summary>
        public void AddSelectedTag()
        {
            if(SelectOtherTag != null)
            {
                SelectFoto.Persons.Add((Person)SelectOtherTag);
            }
        }

        /// <summary>
        /// Удаление выбранного тега с фото
        /// </summary>
        public void DeleteSelectedTag()
        {
            if (SelectFotoTag != null)
            {
                SelectFoto.Persons.Remove((Person)SelectFotoTag);
            }
        }

        #endregion

        #endregion


    }
}
