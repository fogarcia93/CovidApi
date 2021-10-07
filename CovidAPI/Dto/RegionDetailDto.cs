using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CovidAPI.Features
{
    public class RegionDetailDto
    {
        [JsonProperty("iso")]
        public string Iso { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}