using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CovidAPI.Features
{
    public class ProvinceHeadDto
    {
        [JsonProperty("data")]
        public List<ProvincesDetailDto> Data { get; set; }
    }
}