using API_COVID19.BusinessLogic;
using API_COVID19.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_COVID19.Controllers
{
    [ApiController]
    [Route("api/WorldMap")]
    public class WorldMapController : Controller
    {
        private readonly WorldMapBussinesLogic _db;

        public WorldMapController(ApplicationDbContext AppDataContext)
        {
            _db = new WorldMapBussinesLogic(AppDataContext);
        }

        [HttpGet]
        [Route("WorldMapData")]
        public async Task<IActionResult> WorldMapData()
        {

            try
            {
                var WorldMapData = _db.GetWorldMapData();

                return Ok(WorldMapData);
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }


    }
}
