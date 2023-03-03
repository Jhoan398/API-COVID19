using API_COVID19.BusinessLogic;
using API_COVID19.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static API_COVID19.Models.ApiResponse;

namespace API_COVID19.Controllers
{
    [ApiController]
    [Route("UpdateData")]
    public class UpdateFileController : Controller
    {
        private readonly UpdateFileBusinessLogic _UFContext;

        public UpdateFileController(ApplicationDbContext AppDataContext) 
        {
            _UFContext = new UpdateFileBusinessLogic(AppDataContext);
        }

        [HttpGet]
        [Route("GetLastDailyReportFile")]
        public async Task<IActionResult> GetLastDailyReportFile(string dateReport)
        {
            try
            {
                //var dateReport = "01-0-2021";
                var FileName = _UFContext.RepoDataCovidUrl + dateReport + _UFContext.TypeCsvExt;
                var FileNameUSA = _UFContext.RepoUSADataCovidUrl + dateReport + _UFContext.TypeCsvExt;

                string[] ContentUSA = await _UFContext.GetStringCsvFile(FileNameUSA);
                var DataUSA = _UFContext.GetListDataCovidUSA(ContentUSA, dateReport);

                string[] Content = await _UFContext.GetStringCsvFile(FileName);
                var DataCountries = _UFContext.GetListDataCovid(Content, dateReport);

                if (!DataUSA.Any() || !DataCountries.Any())
                    throw new Exception();


                var DicDataCountries = new Dictionary<string, List<DataCovid>>
                {
                    { "AllWorld", DataCountries },
                    { "USA", DataUSA}
                };
               

                foreach (var item in DicDataCountries)
                {
                   await _UFContext.SaveListDBData(item.Value);
                }

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        [HttpGet]
        [Route("GetDataCountries")]
        public async Task<IActionResult> LoadCountries()
        {
            try
            {
                var ListCountries = await _UFContext.GetCountriesStructure();
                if (ListCountries.Count == 0)
                    throw new Exception();

               // _db.SaveDBData(ListCountries);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }
    }
}
