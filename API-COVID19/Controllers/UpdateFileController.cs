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

                var WorldWideCases = await _UFContext.GetWorldWideCases(dateReport);
                
                if (!WorldWideCases.Any())
                    throw new Exception();

                foreach (var Cases in WorldWideCases)
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

                var Vaccinateds = await _UFContext.GetListVaccinateds();

                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetCountriesStructureData")]
        public async Task<IActionResult> LoadCountries()
        {
            try
            {
                var ListCountries = await _UFContext.GetCountriesStructure();
                if (ListCountries.Count == 0)
                    throw new Exception();

                _UFContext.SaveDBData(ListCountries);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }
    }
}
