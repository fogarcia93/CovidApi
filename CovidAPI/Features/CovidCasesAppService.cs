using CovidAPI.Dto;
using CovidAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CovidAPI.Features
{
    public class CovidCasesAppService
    {

        public List<RegionModel> GetAllRegions()
        {
            List<RegionModel> Regions = new List<RegionModel>();
            WebRequest httpRequest = WebRequest.Create($"https://covid-19-statistics.p.rapidapi.com/regions");

            httpRequest.Method = "GET";
            httpRequest.PreAuthenticate = true;
            httpRequest.Headers["X-RapidAPI-Host"] = "covid-19-statistics.p.rapidapi.com";
            httpRequest.Headers["X-RapidAPI-Key"] = "4b56d5e6b9mshe516ac3ce9cb299p1dae3cjsn61584d9b7d9b";
            httpRequest.Headers["ContentType"] = "application/json";

            HttpWebResponse response = (HttpWebResponse)httpRequest.GetResponse();
            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                string result = stream.ReadToEnd();
                stream.Close();
                RegionHeadDto regionRequest = JsonConvert.DeserializeObject<RegionHeadDto>(result);

                foreach (RegionDetailDto item in regionRequest.Data)
                {
                    RegionModel region = new RegionModel
                    {
                        Iso = item.Iso,
                        Name =item.Name
                    };
                    Regions.Add(region);
                }
               
            }
            return Regions;
        }


        public async Task<List<CitiesModel>> GetCovidCasesByRegionAndProvince(string region, string iso)
        {
            List<CitiesModel> cities = new List<CitiesModel>();
            var client = new HttpClient();
            var request = new HttpRequestMessage
              {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://covid-19-statistics.p.rapidapi.com/reports?iso={iso}&region_name={region}"),
                    Headers =
                    {
                        { "x-rapidapi-host", "covid-19-statistics.p.rapidapi.com" },
                        { "x-rapidapi-key", "4b56d5e6b9mshe516ac3ce9cb299p1dae3cjsn61584d9b7d9b" },
                        { "ContentType", "application/json" }
                    },
              };
             using (var response = await client.SendAsync(request))
            {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    CitiesHeadDto city = JsonConvert.DeserializeObject<CitiesHeadDto>(body);
                foreach (var item in city.Data)
                {
                    var cityModel = new CitiesModel
                    {
                        Confirmed = item.confirmed,
                        Deaths = item.deaths,
                        Iso = item.region.Iso,
                        Name = item.region.Name,
                        Province = item.region.Province
                    };

                    cities.Add(cityModel);
                }
              }
            return cities;
        }

        public async Task<CitiesModel> GetCovidCasesByRegion(string region, string iso)
        {
            var response = await GetCovidCasesByRegionAndProvince(region, iso);
            if (response.Count!=0)
            {
                var data = new CitiesModel
                {
                    Confirmed = response.Sum(x => x.Confirmed),
                    Deaths = response.Sum(x => x.Deaths),
                    Name = response.FirstOrDefault().Name,
                    Iso = response.FirstOrDefault().Iso

                };
                return data;
            }
            return new CitiesModel{ };
        }


        public async Task<List<CitiesModel>> GetCovidCasesByProvice(string region, string iso)
        {
            List<CitiesModel> cities = new List<CitiesModel>();
            var response = await GetCovidCasesByRegionAndProvince(region, iso);
            foreach (var item in response)
            {
                var data = new CitiesModel
                {
                    Confirmed = item.Confirmed,
                    Deaths =item.Deaths,
                    Name = item.Province,

                };
                cities.Add(data);
            }
            return cities;
        }

        public async Task<Response> Top10CasesByRegion()
        {
            List<CitiesModel> cities = new List<CitiesModel>();
            List<Codes> codes = new List<Codes>();
            var regions = GetAllRegions();
            foreach (var item in regions)
            {
                var region = await GetCovidCasesByRegion(item.Name, item.Iso);
                cities.Add(region);
            }
            var filter = cities.OrderByDescending(x => x.Confirmed).Take(10);
            foreach (var item in filter)
            {
                var code = new Codes
                {
                    Iso = item.Iso,
                    Name = item.Name
                };
                codes.Add(code);
            }

            return new Response { Regions = filter.ToList(), Codes = codes };
        }

        public async Task<List<CitiesModel>> Top10CasesByProvince(string region, string iso)
        {
            List<CitiesModel> cities = new List<CitiesModel>();
            var provices = await GetCovidCasesByProvice(region, iso);
            foreach (var item in provices)
            {
                cities.Add(item);
            }
            var filter = cities.OrderByDescending(x => x.Confirmed).Take(10);
            return filter.ToList();
        }
    }
}