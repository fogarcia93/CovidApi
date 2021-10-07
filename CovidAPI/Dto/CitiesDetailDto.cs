using CovidAPI.Features;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CovidAPI.Dto
{
    public class CitiesDetailDto
    {
        [JsonProperty("confirmed")]
        public int? confirmed { get; set; }

        [JsonProperty("deaths")]
        public int? deaths { get; set; }

        [JsonProperty("region")]
        public ProvincesDetailDto region { get; set; }


    }
}