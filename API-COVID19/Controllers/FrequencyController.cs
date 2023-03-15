using API_COVID19.BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_COVID19.Controllers
{
    [ApiController]
    [Route("CalculateFrecuency")]
    public class FrequencyController : Controller
    {
        private readonly FrequencyBusinessLogic _UFContext;

        public FrequencyController(ApplicationDbContext AppDataContext)
        {
            _UFContext = new FrequencyBusinessLogic(AppDataContext);
        }

        [HttpGet]
        [Route("GetDataFrecuency")]
        public async Task<IActionResult> GetDataFrecuency() 
        {
            try
            {
                var FrecuencyData = await _UFContext.FrecuencyByCountry();
                var FrecuencyList = FrecuencyData.Values.SelectMany(casesList => casesList).ToList();

                _UFContext.SaveFrequencyDataToDB(FrecuencyList);
              
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest(string.Empty);
            }
            


        }

    }
}
