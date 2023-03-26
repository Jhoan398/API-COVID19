using API_COVID19.BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_COVID19.Controllers
{
    [ApiController]
    [Route("api/Frequency")]
    public class FrequencyController : Controller
    {
        private readonly FrequencyBusinessLogic _UFContext;

        public FrequencyController(ApplicationDbContext AppDataContext)
        {
            _UFContext = new FrequencyBusinessLogic(AppDataContext);
        }


        [HttpGet]
        [Route("FrequencyTypes")]
        public async Task<IActionResult> CountryFrecuencyByTypeFrequency()
        {
            try
            {
                var TypesFrequency = await _UFContext.FrequencyTypes();

                return Ok(TypesFrequency);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }


        [HttpGet]
        [Route("CountryFrequencyByTypeFrequency")]
        public async Task<IActionResult> CountryFrecuencyByTypeFrecuency(int TypeFrequency, int countryID, DateTime? Date) 
        {
            try
            {
                var FecuencyData = await _UFContext.GetFrecuencyByTypeFrecuency(TypeFrequency, countryID, Date);

                return Ok(FecuencyData);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

    }
}
