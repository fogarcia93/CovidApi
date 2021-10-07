using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CovidAPI.Models
{
    public class Response
    {
        public List<CitiesModel> Regions { get; set; }
        public object Codes { get; set; }

    }
}