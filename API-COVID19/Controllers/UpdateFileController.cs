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
        [Route("GetDateReportCases")]
        public async Task<IActionResult> GetDateReportCases(string dateReport)
        {
            try
            {
                var FileName = _UFContext.RepoDataCovidUrl + dateReport + _UFContext.TypeCsvExt;
                var FileNameUSA = _UFContext.RepoUSADataCovidUrl + dateReport + _UFContext.TypeCsvExt;

                string[] ContentUSA = await _UFContext.GetStringCsvFile(FileNameUSA);
                var USACases = _UFContext.GetListCasesCovidUSA(ContentUSA, dateReport);

                string[] Content = await _UFContext.GetStringCsvFile(FileName);
                var WorldCases = _UFContext.GetListDataCovid(Content, dateReport);

                
                if (!USACases.Any() || !WorldCases.Any())
                    throw new Exception();


                var dicCases = new Dictionary<string, List<Cases>>
                {
                    { "AllWorld", WorldCases },
                    { "USA", USACases}
                };


                foreach (var Cases in dicCases)
                {
                    await _UFContext.SaveCasesToDB(Cases.Value);
                }

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        [HttpGet]
        [Route("GetDateReportVaccinateds")]
        public async Task<IActionResult> GetDateReportVaccinateds()
        {
            try
            {



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
