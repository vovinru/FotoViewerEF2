using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryFotoEF
{
    public class Person
    {
        public int PersonId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public ICollection<Foto> Fotoes
        {
            get;
            set;
        }

        public Person()
        {
            Fotoes = new List<Foto>();
        }

        public override string ToString()
        {
            return String.Format("{0} ({1})", Name, Fotoes.Count);
        }
    }
}
