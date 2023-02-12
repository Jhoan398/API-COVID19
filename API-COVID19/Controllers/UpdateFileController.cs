using API_COVID19.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Diagnostics.Metrics;

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
                    string[] lines = fileContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                    List<Country> Countries = new List<Country>();
                   
                    try
                    {
                        foreach (var line in lines.Skip(1))
                        {
                            
                            var values = line.Split(',');
                            var UID = int.Parse(values[0]);
                            var Code3 = string.IsNullOrEmpty(values[3]) ? UID : int.Parse(values[3]);

                            if (Code3 == 840)
                                break;

                            var currentCountry = Countries.Find(x => x.Id == Code3);

                            if (currentCountry == null)
                            {
                                var countryRegion = String.IsNullOrEmpty(values[7]) ? String.Empty : values[7];

                                var country = new Country
                                {
                                    Id = UID,
                                    Country_Name = countryRegion,

                                };

                                Countries.Add(country);
                            }
                            else
                            {
                                var Province_Name = String.IsNullOrEmpty(values[6]) ? String.Empty : values[6];
                                var ProvinceState = new ProvinceState
                                {
                                    Id = UID,
                                    CountryId = currentCountry.Id,
                                    ProvinceName = Province_Name,
                                    Country = currentCountry,
                                };

                                currentCountry.ProvinceStates.Add(ProvinceState);

                            }
                            Console.WriteLine(line);
                        }
                    }
                    catch (Exception e)
                    {

                        throw;
                    }
                    

                }

                return Ok("File downloaded succesfully");
            }
        }
    }
}
