using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryFotoEF
{
    /// <summary>
    /// Обертка фото на топе (сохранение результатов)
    /// </summary>
    public class FotoTop
    {
        public int FotoTopId
        {
            get;
            set;
        }

        /// <summary>
        /// Позиция в рейтенге на момент формирования топа
        /// </summary>
        public int OrderPlace
        {
            get;
            set;
        }



        public int Raiting
        {
            get;
            set;
        }

        public int CountGame
        {
            get;
            set;
        }

        public int CountWin
        {
            get;
            set;
        }

        public int? TopId
        {
            get;
            set;
        }
        public Top Top
        {
            get;
            set;
        }

        /// <summary>
        /// Ссылка на фото которое здесь играет
        /// </summary>
        public int? FotoId
        {
            get;
            set;
        }

        public Foto Foto
        {
            get;
            set;
        }
    }
}
