using API_COVID19.BusinessLogic;
using API_COVID19.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static API_COVID19.Models.ApiResponse;

namespace API_COVID19.Controllers
{
    [ApiController]
    [Route("api/UpdateData")]
    public class UpdateFileController : Controller
    {
        private readonly UpdateFileBusinessLogic _UFContext;

        public UpdateFileController(ApplicationDbContext AppDataContext) 
        {
            _UFContext = new UpdateFileBusinessLogic(AppDataContext);
        }

        //[HttpGet]
        //[Route("LoadWorldMapData")]
        //public async Task<IActionResult> DataFrecuency()
        //{
        //    try
        //    {
        //        var WorldMapData = await _UFContext.GetWorldData();


        //        _UFContext.SaveWorldMapDataToDB(WorldMapData);
        //        //_UFContext.SaveFrequencyDataToDB(FrecuencyList);

        //        return Ok();
        //    }
        //    catch (Exception)
        //    {

        //        return BadRequest(string.Empty);
        //    }
        //}


        //[HttpGet]
        //[Route("GetAllCovidCases")]
        //public async Task<IActionResult> GetDateReportCases() 
        //{
        //    try
        //    {
        //        var startDate = new DateTime(2021, 1, 1);
        //        var endDate = new DateTime(2023, 3, 9); ; // Ayer

        //        var dates = Enumerable.Range(0, (endDate - startDate).Days + 1)
        //                                               .Select(day => startDate.AddDays(day))
        //                                               .ToList();

        //        var dicCases = new Dictionary<string, List<Cases>>();
        //        var WorldWideList = new List<Cases>();

        //        foreach (var DateReport in dates)
        //        {
        //            var WorldWideCases = await _UFContext.GetWorldWideCases(DateReport);
        //            WorldWideList = WorldWideCases.Values.SelectMany(casesList => casesList).ToList();
        //            dicCases.Add(DateReport.ToString("dd-MM-yyyy"), WorldWideList);
        //        }

        //        WorldWideList = dicCases.Values.SelectMany(casesList => casesList).ToList();
        //        await _UFContext.SaveCountriesCasesToDB(WorldWideList);


        //        return Ok();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //[HttpGet]
        //[Route("CalculateDataFrecuency")]
        //public async Task<IActionResult> DataFrecuency()
        //{
        //    try
        //    {
        //        var FrecuencyData = await _UFContext.FrecuencyByCountry();
        //        var FrecuencyList = FrecuencyData.Values.SelectMany(casesList => casesList).ToList();

        //        _UFContext.SaveFrequencyDataToDB(FrecuencyList);

        //        return Ok();
        //    }
        //    catch (Exception)
        //    {

        //        return BadRequest(string.Empty);
        //    }



        //}

        //[HttpGet]
        //[Route("GetDateReportVaccinateds")]
        //public async Task<IActionResult> GetDateReportVaccinateds()
        //{
        //    try
        //    {

        //        var Vaccinateds = await _UFContext.GetListVaccinateds();
        //        if (!Vaccinateds.Any())
        //            throw new Exception();


        //        await _UFContext.SaveCountriesVaccinatedToDB(Vaccinateds);

        //        return Ok();
        //    }
        //    catch (Exception)
        //    {

        //        return BadRequest();
        //    }
        //}

        //[HttpGet]
        //[Route("GetCountriesStructureData")]
        //public async Task<IActionResult> LoadCountries()
        //{
        //    try
        //    {
        //        var ListCountries = await _UFContext.GetCountriesStructure();
        //        if (ListCountries.Count == 0)
        //            throw new Exception();

        //        _UFContext.SaveCountriesStructureToDB(ListCountries);
        //        return Ok();
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }

        //}


        //[HttpGet]
        //[Route("LoadISOCountries")]
        //public async Task<IActionResult> LoadISOCountries()
        //{
        //    try
        //    {
        //        var ListCountries = await _UFContext.LoadISO3();
        //        _UFContext.SaveISOStructureToDB(ListCountries);
        //        return Ok();
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }

        //}

    }
}
