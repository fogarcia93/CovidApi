using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CovidAPI.Models
{
    public class CitiesModel
    {
        public int? Confirmed { get; set; }

        public int? Deaths { get; set; }

        public string Iso { get; set; }

        public string Name { get; set; }

        public string Province { get; set; }

    }
}