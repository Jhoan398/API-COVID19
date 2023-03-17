using API_COVID19.BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_COVID19.Controllers
{
    [ApiController]
    [Route("Frequency")]
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
        [Route("CountryFrecuencyByTypeFrecuency")]
        public async Task<IActionResult> CountryFrecuencyByTypeFrecuency(int TypeFrecuency, int countryID, DateTime Date) 
        {
            try
            {
                var FecuencyData = await _UFContext.GetFrecuencyByTypeFrecuency(TypeFrecuency, countryID, Date);

                return Ok(FecuencyData);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

    }
}
