using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryFotoEF
{
    public class Foto
    {
        public int FotoId
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        /// <summary>
        /// Количество побед
        /// </summary>
        public int CountWin
        {
            get;
            set;
        }

        /// <summary>
        /// Количество поражений
        /// </summary>
        public int CountLose
        {
            get;
            set;
        }

        /// <summary>
        /// Количество штрафов (пропусков)
        /// </summary>
        public int CountPenalty
        {
            get;
            set;
        }

        /// <summary>
        /// Рейтинг фотографии
        /// </summary>
        public float Raiting
        {
            get;
            set;
        }

        public DateTime? Date
        {
            get;
            set;
        }

        /// <summary>
        /// На сколько градусов крутить при показе
        /// </summary>
        public int Rotate
        {
            get;
            set;
        }


        public int? CityId
        {
            get;
            set;
        }

        /// <summary>
        /// Ссылки на участие в топах
        /// </summary>
        public ICollection<FotoTop> FotoTops
        {
            get;
            set;
        }


        public void CalculateRaiting()
        {
            int CountGame = CountLose + CountWin;
            if(CountGame == 0)
            {
                Raiting = 0;
                return;
            }

            Raiting = ((float)CountWin / (float)CountGame * (Math.Min(CountWin, 100))) + CountWin * 0.0001f;
        }

        /// <summary>
        /// Получить дату съемки из файла
        /// </summary>
        private void GetDateFromFile()
        {
            FileAttributes attrs = File.GetAttributes(FileName);

            FileInfo info = new FileInfo(FileName);

            Date = info.LastWriteTime;
        }

        public string GetFotoInfo()
        {
            if (Date == null)
                GetDateFromFile();

            return
                string.Format("W = {0}, L = {1},\nR = {2}\nДата = {3}", CountWin, CountLose, Raiting, Date);
        }


        public static int EqualsByFileDate(Foto f1, Foto f2)
        {
            if (f1.Date == null)
                f1.GetDateFromFile();
            if (f2.Date == null)
                f2.GetDateFromFile();

            if (f1.Date > f2.Date)
                return 1;
            if (f1.Date < f2.Date)
                return -1;

            return 0;
        }
    }
}
