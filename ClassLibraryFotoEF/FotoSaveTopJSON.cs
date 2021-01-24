using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryFotoEF
{
    /// <summary>
    /// Класс для сохранения в json
    /// </summary>
    public class FotoSaveTopJSON
    {
        public string FileName
        {
            get;
            set;
        }

        public int Place
        {
            get;
            set;
        }

        public int CountWin
        {
            get;
            set;
        }

        public int CountLose
        {
            get;
            set;
        }

        public float Raiting
        {
            get;
            set;
        }
    }
}
