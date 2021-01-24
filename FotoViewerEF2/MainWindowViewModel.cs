using ClassLibraryFotoEF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoViewerEF2
{
    public class MainWindowViewModel
    {
        #region fields


        #endregion

        #region propertyes

        public FotoContext FotoContext
        {
            get;
            set;
        }

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


        #endregion

        #region costructors

        public MainWindowViewModel()
        {
            FotoContext = new FotoContext();
            foreach (Foto f in FotoContext.Fotos)
                f.Date = null;

            LoadFoto();

            CountFoto = FotoContext.Fotos.Count();
            CountGameNow = 0;
        }

        #endregion

        #region methods

        public void LoadFoto()
        {
            Foto foto1 = null;

            while(foto1 == null)
            {
                foto1 = FotoContext.GetRandomFoto();

                if (foto1 == null)
                    return;

                if(foto1.CountPenalty != 0)
                {
                    foto1.CountPenalty--;
                    foto1 = null;
                }
            }

            Foto foto2 = null;

            while(foto2 == null)
            {
                foto2 = FotoContext.GetRandomFoto();
                if(foto1 == foto2)
                {
                    foto2 = null;
                    continue;
                }

                if(foto2.CountPenalty != 0)
                {
                    foto2.CountPenalty--;
                    foto2 = null;
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

            CountWithoutGame = FotoContext.Fotos.Count(f => f.CountWin + f.CountLose == 0);
            CountWithoutLose = FotoContext.Fotos.Count(f => f.CountLose == 0);
            Count0Penalty = FotoContext.Fotos.Count(f => f.CountPenalty == 0);
        }

        public void AddFotoFolder(string directory)
        {
            string [] directories = Directory.GetDirectories(directory);

            foreach (string dir in directories)
                AddFotoFolder(dir);

            string[] files = Directory.GetFiles(directory, "*.jpg");

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
                Foto2.CountPenalty = MathFotoEF.Fibonachi(Foto2.CountLose) + 30;

                CountGameNow++;
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
                Foto1.CountPenalty = MathFotoEF.Fibonachi(Foto1.CountLose) + 30;

                CountGameNow++;
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

        #endregion


    }
}
