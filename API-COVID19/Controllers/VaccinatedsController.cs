using API_COVID19.BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_COVID19.Controllers
{
    [ApiController]
    [Route("api/Vaccinateds")]
    public class VaccinatedsController : Controller
    {
        private readonly VaccinatedsBusinessLogic _db;

        public VaccinatedsController(ApplicationDbContext AppDataContext)
        {
            _db = new VaccinatedsBusinessLogic(AppDataContext);
        }



        [HttpGet]
        [Route("CountryVaccinatedsByDateReport")]
        public async Task<IActionResult> CountryVaccinatedsByDateReport(int CountryId, DateTime InitialDate, DateTime FinalDate)
        {

            try
            {
                InitialDate= InitialDate.Date.ToUniversalTime();
                FinalDate = FinalDate.Date.ToUniversalTime();
                var Vaccinateds = await _db.GetVaccinatedsByDateReport(CountryId, InitialDate, FinalDate);
                return Ok(Vaccinateds);
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }
    }
}
