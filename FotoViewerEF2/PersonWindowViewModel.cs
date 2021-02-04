using ClassLibraryFotoEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoViewerEF2
{
    public class PersonWindowViewModel:BaseViewModel
    {

        public Person Person
        {
            get;
            set;
        }

        public string PersonName
        {
            get
            {
                return Person.Name;
            }
            set
            {
                Person.Name = value;
            }
        }

        public PersonWindowViewModel(FotoContext fotoContext, Person person) :
            base(fotoContext)
        {
            Person = person;
        }
    }
}
