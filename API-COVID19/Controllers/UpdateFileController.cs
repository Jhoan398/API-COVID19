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
        [Route("GetAllCovidCases")]
        public async Task<IActionResult> GetDateReportCases() 
        {
            try
            {
                var startDate = new DateTime(2021, 1, 1);
                var endDate = new DateTime(2023, 3, 9); ; // Ayer

                var dates = Enumerable.Range(0, (endDate - startDate).Days + 1)
                                                       .Select(day => startDate.AddDays(day))
                                                       .ToList();

                var dicCases = new Dictionary<string, List<Cases>>();
                var WorldWideList = new List<Cases>();

                foreach (var DateReport in dates)
                {
                    var WorldWideCases = await _UFContext.GetWorldWideCases(DateReport);
                    WorldWideList = WorldWideCases.Values.SelectMany(casesList => casesList).ToList();
                    dicCases.Add(DateReport.ToString("dd-MM-yyyy"), WorldWideList);
                }

                WorldWideList = dicCases.Values.SelectMany(casesList => casesList).ToList();
                await _UFContext.SaveCountriesCasesToDB(WorldWideList);


                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet]
        [Route("GetDateReportCases")]
        public async Task<IActionResult> GetDateReportCases(DateTime dateReport)
        {
            try
            {
                var WorldWideCases = await _UFContext.GetWorldWideCases(dateReport);
                
                if (!WorldWideCases.Any())
                    throw new Exception();

                foreach (var Cases in WorldWideCases)
                {
                    await _UFContext.SaveCountriesCasesToDB(Cases.Value);
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
                if (!Vaccinateds.Any())
                    throw new Exception();


                await _UFContext.SaveCountriesVaccinatedToDB(Vaccinateds);

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

                _UFContext.SaveCountriesStructureToDB(ListCountries);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }
    }
}
