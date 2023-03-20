using API_COVID19.BusinessLogic;
using API_COVID19.Models;
using Microsoft.AspNetCore.Mvc;
using static API_COVID19.Models.ApiResponse;

namespace API_COVID19.Controllers
{
    [ApiController]
    [Route("api/Cases")]
    public class CasesController : Controller
    {
        private readonly CasesBusinessLogic _db;

        public CasesController(ApplicationDbContext AppDataContext)
        {
            _db = new CasesBusinessLogic(AppDataContext);
        }

        [HttpGet]
        [Route("CountryCasesByConditions")]
        public async Task<IActionResult> CountryCasesByConditions(int CountryId, int? PronvinceSId , bool IsSumattion, DateTime InitialDate, DateTime FinalDate)
        {

            try
            {
                var cases = new List<Cases>();
                InitialDate = InitialDate.ToUniversalTime();
                FinalDate = FinalDate.ToUniversalTime();

                if (IsSumattion) 
                {
                    cases = await _db.GetSumCasesByConditions(CountryId, PronvinceSId, InitialDate, FinalDate);
                }
                else 
                {
                    cases = await _db.GetListCasesByConditions(CountryId, PronvinceSId, InitialDate, FinalDate);
                }



                return Ok(cases);
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }
    }
}
