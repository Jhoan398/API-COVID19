using Microsoft.AspNetCore.Mvc;

namespace API_COVID19.Controllers
{
    [ApiController]
    [Route("UpdateData")]
    public class UpdateFileController : Controller
    {
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
            using (var client = new HttpClient())
            {
                var urlFile = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/UID_ISO_FIPS_LookUp_Table.csv";

                using (var response = await client.GetAsync(urlFile))
                {
                    response.EnsureSuccessStatusCode();
                    var fileContent = await response.Content.ReadAsStringAsync();

                }

                return Ok("File downloaded succesfully");
            }
        }
    }
}
