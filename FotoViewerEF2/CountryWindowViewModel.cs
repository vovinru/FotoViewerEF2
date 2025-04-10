﻿using ClassLibraryFotoEF;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoViewerEF2
{
    public class CountryWindowViewModel : BaseViewModel
    {

        public Country Country
        {
            get;
            set;
        }

        public string CountryName
        {
            get
            {
                return Country.Name;
            }
            set
            {
                Country.Name = value;
            }
        }

        public CountryWindowViewModel(FotoContext fotoContext, Country country) :
            base(fotoContext)
        {
            Country = country;
        }
    }
}
