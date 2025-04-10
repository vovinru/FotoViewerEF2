using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryFotoEF
{
    /// <summary>
    /// Класс описывающий отчет по штрафам
    /// </summary>
    public class PenaltyReport
    {
        /// <summary>
        /// Словарь с описанием количества разных штрафов. Ключ - размер штрафа, значение - их количество
        /// </summary>
        public Dictionary<int, int> PenaltyDictionary
        {  
            get; 
            set; 
        }

        /// <summary>
        /// Суммарный штраф до разрыва
        /// </summary>
        public int SumPenaltyUndoSpace
        {
            get;
            set;
        }

        /// <summary>
        /// Суммарный штраф всех фото
        /// </summary>
        public int SumPenalty
        {
            get;
            set;
        }

        public PenaltyReport() 
        {
            PenaltyDictionary = new Dictionary<int, int>();
        }

        public void AddPenalty(int penalty) 
        {
            if(!PenaltyDictionary.ContainsKey(penalty))
                PenaltyDictionary.Add(penalty, 0);
            PenaltyDictionary[penalty]++;  
        }

        public void CalculationResult()
        {
            SumPenaltyUndoSpace = 0;
            SumPenalty = 0;
            bool space = false;


            int maxPenalty = PenaltyDictionary.Keys.Max();
            for (int i = 0; i < maxPenalty; i++)
            {
                if (PenaltyDictionary.ContainsKey(i))
                {
                    if (!space)
                        SumPenaltyUndoSpace += PenaltyDictionary[i] * i;
                    SumPenalty += PenaltyDictionary[i] * i;
                }
                else
                    space = true;
            }
        }
    }
}
