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
    [Route("api/Country")]
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

            try
            {
                var countries = await _db.GetCountries();

                return Ok(countries);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetCountriesByName")]
        public async Task<IActionResult> GetCountriesByName(string name)
        {

            try
            {
                var country = await _db.GetCountryByNameAsync(name);

                return Ok(country);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetCountriesById")]
        public async Task<IActionResult> GetCountriesById(int UID)
        {


            try
            {
                var country = await _db.GetCountryById(UID);

                return Ok(country);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }





    }
}
