using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CovidAPI.Features
{
    [RoutePrefix("api/covid")]
    public class CovidCasesController : ApiController
    {
        private readonly CovidCasesAppService _covidCasesAppService;

        public CovidCasesController(CovidCasesAppService covidCasesAppService)
        {
            _covidCasesAppService = covidCasesAppService;
        }


        [HttpGet]
        [Route("get-regions")]
        public IHttpActionResult GetRegionsFromApi()
        {
            var response = _covidCasesAppService.GetAllRegions();
            return Ok(response);
        }


        [HttpGet]
        [Route("get-cases")]
        public async Task<IHttpActionResult> GetCasesByCountryFromApi(string region, string iso)
        {
            return Ok(await _covidCasesAppService.GetCovidCasesByRegionAndProvince(region, iso));
        }

        [HttpGet]
        [Route("get-cases-by-region")]
        public async Task<IHttpActionResult> GetCasesBy(string region, string iso)
        {
            return Ok(await _covidCasesAppService.GetCovidCasesByRegion(region, iso));
        }

        [HttpGet]
        [Route("get-cases-by-province")]
        public async Task<IHttpActionResult> GetCasesByProvince(string region, string iso)
        {
            return Ok(await _covidCasesAppService.GetCovidCasesByProvice(region, iso));
        }

        [HttpGet]
        [Route("get-top-cases-by-region")]
        public async Task<IHttpActionResult> GetTopCasesByRegion()
        {
            return Ok(await _covidCasesAppService.Top10CasesByRegion());
        }

        [HttpGet]
        [Route("get-top-cases-by-province")]
        public async Task<IHttpActionResult> GetTopCasesByProvince(string region, string iso)
        {
            return Ok(await _covidCasesAppService.Top10CasesByProvince(region, iso));
        }
    }
   
}
