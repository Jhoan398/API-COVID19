using API_COVID19.BusinessLogic;
using API_COVID19.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static API_COVID19.Models.ApiResponse;

namespace API_COVID19.Controllers
{
    public class ProvinceStatesController : Controller
    {
        private readonly ProvinceStateBusinessLogic _db;

        public ProvinceStatesController(ApplicationDbContext AppDataContext)
        {
            _db = new ProvinceStateBusinessLogic(AppDataContext);
        }


        [HttpGet]
        [Route("GetProvinceStatesByIdCountry")]
        public async Task<IActionResult> GetProvinceStatesByIdCountry(int UID)
        {
            var type = ResponseType.Succes;

            try
            {
                IEnumerable<ProvinceState> ProvinceStates = _db.GetProvinceStatesFromCountry(UID);

                if (!ProvinceStates.Any())
                    type = ResponseType.NotFound;

                return Ok(ResponseHandler.GetAppResponse(type, ProvinceStates));
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
