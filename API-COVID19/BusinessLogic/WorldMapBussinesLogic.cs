using API_COVID19.Models;

namespace API_COVID19.BusinessLogic
{
    public class WorldMapBussinesLogic
    {

        private ApplicationDbContext _dbContext;

        public WorldMapBussinesLogic(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public List<WorldmapData> GetWorldMapData() 
        {
            return _dbContext.WorldmapData.ToList();
        }
    }
}
