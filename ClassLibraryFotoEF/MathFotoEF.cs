using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryFotoEF
{
    public static class MathFotoEF
    {
        /// <summary>
        /// Функция считает число в ряду фибоначи
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Fibonachi(int value)
        {
            int result = 0;
            for (int i = 1; i <= value; i++)
            {
                result += i;
            }

            return result;
        }
    }
}
