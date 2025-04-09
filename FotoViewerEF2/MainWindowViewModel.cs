using ClassLibraryFotoEF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoViewerEF2
{
    public class MainWindowViewModel:BaseViewModel
    {
        #region fields


        #endregion

        #region propertyes

        //parameters for fotos
        /// <summary>
        /// Параметр включает режим работы с победителями. Фотография победившая попадает в топ20, 
        /// если в наборе уже 20 фото, то 1й фоткой будет один из бывших победителей
        /// </summary>
        public bool Top20
        {
            get;
            set;
        }

        //---

        //meta data for foto

        /// <summary>
        /// Набор свежих победителей
        /// </summary>
        List<Foto> Top20Winners
        {
            get;
            set;
        }

        //---


        public Foto Foto1
        {
            get;
            set;
        }

        public Foto Foto2
        {
            get;
            set;
        }

        public string Foto1File
        {
            get;
            set;
        }

        public string Foto2File
        {
            get;
            set;
        }

        public string Foto1Info
        {
            get;
            set;
        }

        public string Foto2Info
        {
            get;
            set;
        }

        public int Foto1Angle
        {
            get
            {
                if (Foto1 != null)
                    return Foto1.Rotate;

                return 0;
            }
        }

        public int Foto2Angle
        {
            get
            {
                if (Foto2 != null)
                    return Foto2.Rotate;

                return 0;
            }
        }

        /// <summary>
        /// Количество фото в БД (которые играют)
        /// </summary>
        public int CountFoto
        {
            get;
            set;
        }

        /// <summary>
        /// Количество сыгранных за сессию игр
        /// </summary>
        public int CountGameNow
        {
            get;
            set;
        }

        /// <summary>
        /// Количество без игр
        /// </summary>
        public int CountWithoutGame
        {
            get;
            set;
        }

        /// <summary>
        /// Количество без пораженией
        /// </summary>
        public int CountWithoutLose
        {
            get;
            set;
        }

        /// <summary>
        /// Количество фото без штрафа
        /// </summary>
        public int Count0Penalty
        {
            get;
            set;
        }

        /// <summary>
        /// Выбираем фото из фильтра (если фильтр задан)
        /// </summary>
        public List<Foto> FotosFilter
        {
            get;
            set;
        }

        public Filter Filter
        {
            get;
            set;
        }


        #endregion

        #region costructors

        public MainWindowViewModel() :
            base(new FotoContext())
        {
            foreach (Foto f in FotoContext.Fotos)
                f.Date = null;



            Top20 = true;
            Top20Winners = new List<Foto>();

            FotosFilter = FotoContext.GetFotosByFilter(new Filter());

            LoadFoto();

            CountFoto = FotoContext.Fotos.Count();
            CountGameNow = 0;

        }

        #endregion

        #region methods

        public void LoadFoto()
        {
            int newCount = FotosFilter.Count(f => f.CountWin + f.CountLose == 0);
            bool newFoto = newCount > 0;

            Count0Penalty = FotosFilter.Count(f => f.CountPenalty == 0);

            Foto foto1 = null;

            int attempt = 0;
            Random random = new Random();

            while(foto1 == null)
            {
                //Если есть фото которые не играли, они должны сыграть
                if (newCount > 0)
                {
                    Top20Winners.Clear();

                    List<Foto> FotosNew = FotosFilter.FindAll(f => f.CountLose + f.CountWin == 0);
                    int index = random.Next(0, FotosNew.Count - 1);
                    foto1 = FotosNew[index];
                }
                else
                {
                    if (Top20 && Top20Winners.Count == 20)
                    {
                        foto1 = Top20Winners.First();
                        Top20Winners.RemoveAt(0);
                    }

                    else
                    {
                        int index = random.Next(0, FotosFilter.Count - 1);
                        foto1 = FotosFilter[index];

                        if (!File.Exists(foto1.FileName))
                        {
                            FotoContext.DeleteFoto(foto1);
                            FotosFilter.Remove(foto1);
                            foto1 = null;
                            continue;
                        }

                        if (foto1 == null)
                            return;

                        if (foto1.CountPenalty != 0)
                        {
                            foto1.CountPenalty -= Math.Min((1 + attempt / 10000), foto1.CountPenalty);
                            foto1 = null;
                            attempt++;
                            continue;
                        }
                    }
                }
            }

            Foto foto2 = null;

            while(foto2 == null)
            {

                int index = random.Next(0, FotosFilter.Count - 1);
                foto2 = FotosFilter[index];


                if (!File.Exists(foto2.FileName))
                {
                    FotoContext.DeleteFoto(foto2);
                    FotosFilter.Remove(foto2);
                    foto2 = null;
                    continue;
                }

                if (foto1 == foto2)
                {
                    foto2 = null;
                    continue;
                }

                if(foto2.CountPenalty != 0)
                {
                    foto2.CountPenalty -= Math.Min((1 + attempt / 10000), foto2.CountPenalty);
                    //fotoBlock.Remove(foto2);
                    foto2 = null;
                    attempt++;
                }
            }

            Foto1 = foto1;
            Foto2 = foto2;

            Foto1.CalculateRaiting();
            Foto2.CalculateRaiting();

            Foto1File = foto1.FileName;
            Foto2File = foto2.FileName;

            Foto1Info = foto1.GetFotoInfo();
            Foto2Info = foto2.GetFotoInfo();

            CountWithoutGame = FotosFilter.Count(f => f.CountWin + f.CountLose == 0);
            CountWithoutLose = FotosFilter.Count(f => f.CountLose == 0);
            CountFoto = FotosFilter.Count();
        }

        public void AddFotoFolder(string directory)
        {
            string [] directories = Directory.GetDirectories(directory);

            foreach (string dir in directories)
                AddFotoFolder(dir);

            List<string> files = Directory.GetFiles(directory, "*.*").
                Where(s => s.EndsWith(".jpg") || s.EndsWith(".JPG") || s.EndsWith(".jpeg") || s.EndsWith(".jfif")).ToList();

            foreach(string file in files)
            {
                if (!FotoContext.ContainFoto(file))
                    FotoContext.AddFoto(file);
            }

            FotoContext.SaveChanges();
            LoadFoto();

            CountFoto = FotoContext.Fotos.Count();
        }

        public void ClickFoto1()
        {
            if (Foto1 != null && Foto2 != null)
            {
                Foto1.CountWin++;
                Foto2.CountLose++;
                Foto2.CountPenalty = MathFotoEF.Fibonachi(Foto2.CountLose) + FotoContext.GetBasePenalty(Count0Penalty);

                if(Foto1.CountWin > 50)
                    Foto1.CountPenalty += (Foto1.CountWin - 40) / 10;
                if (Foto2.CountWin > 50)
                    Foto2.CountPenalty += (Foto2.CountWin - 40) / 10;

                if (Foto1.CountWin > 100)
                    Foto1.CountPenalty += (Foto1.CountWin - 100);
                if (Foto2.CountWin > 100)
                    Foto2.CountPenalty += (Foto2.CountWin - 100);

                Foto1.CountPenalty++;
                Foto2.CountPenalty++;

                CountGameNow++;

                if (Top20)
                {
                    Top20Winners.Add(Foto1);
                }
            }

            LoadFoto();

            FotoContext.SaveChanges();
        }

        public void ClickFoto2()
        {
            if (Foto1 != null && Foto2 != null)
            {
                Foto2.CountWin++;
                Foto1.CountLose++;
                Foto1.CountPenalty = MathFotoEF.Fibonachi(Foto1.CountLose) + FotoContext.GetBasePenalty(Count0Penalty);

                if (Foto1.CountWin > 50)
                    Foto1.CountPenalty += (Foto1.CountWin - 40) / 10;
                if (Foto2.CountWin > 50)
                    Foto2.CountPenalty += (Foto2.CountWin - 40) / 10;

                if (Foto1.CountWin > 100)
                    Foto1.CountPenalty += (Foto1.CountWin - 100);
                if (Foto2.CountWin > 100)
                    Foto2.CountPenalty += (Foto2.CountWin - 100);

                CountGameNow++;


                if (Top20)
                {
                    Top20Winners.Add(Foto2);
                    if (Top20Winners.Count == 20)
                        Top20Winners.RemoveAt(0);
                }
            }

            LoadFoto();

            FotoContext.SaveChanges();
        }

        public void RotateFoto1()
        {
            Foto1.Rotate += 90;
            if (Foto1.Rotate >= 360)
                Foto1.Rotate = 0;
        }

        public void RotateFoto2()
        {
            Foto2.Rotate += 90;
            if (Foto2.Rotate >= 360)
                Foto2.Rotate = 0;
        }

        public void DeleteFoto1()
        {
            if (Foto1 != null)
            {
                FotoContext.DeleteFoto(Foto1);
            }

            LoadFoto();
            FotoContext.SaveChanges();
        }

        public void DeleteFoto2()
        {
            if (Foto2 != null)
            {
                FotoContext.DeleteFoto(Foto2);
            }

            LoadFoto();
            FotoContext.SaveChanges();
        }

        public void UpdateFotosFilter(Filter filter)
        {
            Filter = filter;
            FotosFilter = FotoContext.GetFotosByFilter(filter);
        }

        public void ClearPenalty()
        {
            foreach (Foto f in FotoContext.Fotos)
            {
                f.CountPenalty = 0;
            }

            FotoContext.SaveChanges();
        }

        #endregion


    }
}
