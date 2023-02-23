using API_COVID19.BusinessLogic;
using API_COVID19.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics.Metrics;
using System.Text.Json;
using System.Text.Json.Serialization;
using static API_COVID19.Models.ApiResponse;

namespace API_COVID19.Controllers
{
    [ApiController]
    [Route("Country")]
    public class CountryController : Controller
    {
        private readonly CountryBusinessLogic _db;

        public CountryController(ApplicationDbContext AppDataContext)
        {
            _db = new CountryBusinessLogic(AppDataContext);
        }


        [HttpGet]
        [Route("GetCountries")]
        public async Task<IActionResult> GetCountriesAsync() 
        {
            var type = ResponseType.Succes;

            try
            {
                IEnumerable<Country>countries = await _db.GetCountries();

                if (!countries.Any())
                    type = ResponseType.NotFound;

                return Ok(ResponseHandler.GetAppResponse(type, countries));
            }
            catch (Exception)
            {

                throw;
            }
        }




        [HttpGet]
        [Route("GetCountriesByName")]
        public async Task<IActionResult> GetCountriesByName(string name)
        {
            var type = ResponseType.Succes;

            try
            {
                var country = await _db.GetCountryByNameAsync(name);

                if (country == null)
                    type = ResponseType.NotFound;

                return Ok(ResponseHandler.GetAppResponse(type, country));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("GetCountriesById")]
        public async Task<IActionResult> GetCountriesById(int UID)
        {
            var type = ResponseType.Succes;

            try
            {
                var country = _db.GetCountryById(UID);

                if (country == null)
                    type = ResponseType.NotFound;

                return Ok(ResponseHandler.GetAppResponse(type, country));
            }
            catch (Exception)
            {

                throw;
            }
        }





    }
}
