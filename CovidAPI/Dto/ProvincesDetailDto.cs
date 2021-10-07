using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CovidAPI.Features
{
    public class ProvincesDetailDto
    {
        [JsonProperty("iso")]
        public string Iso { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("province")]
        public string Province { get; set; }

        [JsonProperty("lat")]
        public string Latitude { get; set; }

        [JsonProperty("long")]
        public string Longitude { get; set; }
    }
}