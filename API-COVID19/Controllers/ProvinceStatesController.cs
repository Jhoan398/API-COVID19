using API_COVID19.BusinessLogic;
using API_COVID19.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static API_COVID19.Models.ApiResponse;

namespace API_COVID19.Controllers
{
    [ApiController]
    [Route("api/ProvinceStates")]
    public class ProvinceStatesController : Controller
    {
        private readonly ProvinceStateBusinessLogic _db;

        public ProvinceStatesController(ApplicationDbContext AppDataContext)
        {
            _db = new ProvinceStateBusinessLogic(AppDataContext);
        }


        [HttpGet]
        [Route("ProvinceStatesByIdCountry")]
        public async Task<IActionResult> ProvinceStatesByIdCountry(int UID)
        {

            try
            {
                var ProvinceStates = await _db.GetProvinceStatesFromCountry(UID);

                return Ok(ProvinceStates);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("ProvinceStatesById")]
        public async Task<IActionResult> ProvinceStatesById(int UID)
        {

            try
            {
                var ProvinceStates = await _db.GetProvinceStatesById(UID);

                return Ok(ProvinceStates);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GetProvinceStatesByName")]
        public async Task<IActionResult> GetProvinceStatesByName(string Name)
        {

            try
            {
                var ProvinceStates = await _db.GetProvinceStatesByName(Name);

                return Ok(ProvinceStates);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

    }
}
