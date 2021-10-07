using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CovidAPI.Dto
{
    public class CitiesHeadDto
    {
        [JsonProperty("data")]
        public List<CitiesDetailDto> Data { get; set; }
        
    }
}