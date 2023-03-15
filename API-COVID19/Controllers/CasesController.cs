using API_COVID19.BusinessLogic;
using API_COVID19.Models;
using Microsoft.AspNetCore.Mvc;
using static API_COVID19.Models.ApiResponse;

namespace API_COVID19.Controllers
{
    [ApiController]
    [Route("Cases")]
    public class CasesController : Controller
    {
        private readonly CasesBusinessLogic _db;

        public CasesController(ApplicationDbContext AppDataContext)
        {
            _db = new CasesBusinessLogic(AppDataContext);
        }

        [HttpGet]
        [Route("CasesBeetwenDatesByCountryId")]
        public async Task<IActionResult> CasesBeetwenDatesByCountryId(DateTime InitialDate, DateTime FinalDate, int CountryId) 
        {
            var type = ResponseType.Succes;

            try
            {
                var Cases = await _db.GetCasesBeetwenDatesByCountryId(InitialDate.ToUniversalTime(), FinalDate.ToUniversalTime(), CountryId);

                if (Cases == null)  
                    type = ResponseType.NotFound;


                return Ok(ResponseHandler.GetAppResponse(type, Cases));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("CasesByCountryName")]
        public async Task<IActionResult> CasesByCountryName(string CountryName)
        {
            var type = ResponseType.Succes;

            try
            {
                var Cases = _db.GetCasesByCountryName(CountryName);

                if (!Cases.Any())
                    type = ResponseType.NotFound;


                return Ok(ResponseHandler.GetAppResponse(type, Cases));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("CasesByCountryId")]
        public async Task<IActionResult> CasesByCountryId(int CountryID)
        {
            var type = ResponseType.Succes;

            try
            {
                var Cases = await _db.GetCasesByCountryId(CountryID);

                if (!Cases.Any())
                    type = ResponseType.NotFound;

                return Ok(ResponseHandler.GetAppResponse(type, Cases));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("CasesByCountry_ProvinceStateId")]
        public async Task<IActionResult> CasesByCountry_ProvinceStateId(int CountryId, int ProvinceId)
        {
            var type = ResponseType.Succes;

            try
            {
                var Cases = await _db.GetCasesByCountry_PronvinceName(CountryId, ProvinceId);

                if (!Cases.Any())
                    type = ResponseType.NotFound;


                return Ok(ResponseHandler.GetAppResponse(type, Cases));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("CasesByCountry_ProvinceStateNames")]
        public async Task<IActionResult> CasesByCountry_ProvinceStateNames(string CountryName, string ProvinceStateName)
        {
            var type = ResponseType.Succes;

            try
            {
                var Cases = await _db.GetCasesByCountry_PronvinceName(CountryName, ProvinceStateName);

                 if (!Cases.Any())
                    type = ResponseType.NotFound;


                return Ok(ResponseHandler.GetAppResponse(type, Cases));
            }
            catch (Exception)
            {

                throw;
            }
        }




    }
}
