using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CovidAPI.Models
{
    public class ProvinceModel
    {
        public string Iso { get; set; }

        public string Name { get; set; }

        public string Province { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
    }
}