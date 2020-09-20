using System;
using System.Collections.Generic;

namespace CovidStatisticsMobile.Models
{
    public class Covid
    {
        public string Message { get; set; }
        public Global Global { get; set; }
        public IList<CountryModel> Countries { get; set; }
        public DateTime Date { get; set; }
    }
}
