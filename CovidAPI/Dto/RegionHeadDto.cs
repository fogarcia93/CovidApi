using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CovidAPI.Features
{
    public class RegionHeadDto
    {
        [JsonProperty("data")]
        public List<RegionDetailDto> Data { get; set; }

    }
}