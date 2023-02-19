using API_COVID19.BusinessLogic;
using API_COVID19.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Diagnostics.Metrics;
using static API_COVID19.Models.APIResponse;

namespace API_COVID19.Controllers
{
    [ApiController]
    [Route("UpdateData")]
    public class UpdateFileController : Controller
    {
        private readonly CountryBusinessLogic _db;

        public UpdateFileController(ApplicationDbContext AppDataContext) 
        {
            _db = new CountryBusinessLogic(AppDataContext);
        }

        [HttpGet]
        [Route("GetLastDailyReportFile")]
        public async Task<IActionResult> GetLastDailyReportFile() 
        {
            using(var client  = new HttpClient() )
            {
                var urlFile = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_daily_reports/01-11-2023.csv";

                using (var response = await client.GetAsync(urlFile) )
                {
                    response.EnsureSuccessStatusCode();
                    var fileContent = await response.Content.ReadAsStringAsync();

                }

                return Ok("File downloaded succesfully");
            }
        }

        [HttpGet]
        [Route("GetDataCountries")]
        public async Task<IActionResult> GetFile()
        {
            try
            {
                var ListCountries = await _db.GetCountriesFromCsvTable();
                if (ListCountries.Count == 0)
                    throw new Exception();

                _db.SaveDBData(ListCountries);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }
    }
}
