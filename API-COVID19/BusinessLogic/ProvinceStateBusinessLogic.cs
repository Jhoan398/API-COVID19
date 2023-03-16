using API_COVID19.Models;
using System.Linq;

namespace API_COVID19.BusinessLogic
{
    public class ProvinceStateBusinessLogic
    {
        private ApplicationDbContext _dbContext;

        public ProvinceStateBusinessLogic(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ProvinceState> GetProvinceStatesFromCountry(int IdCountry)
        {
            return _dbContext.ProvinceState.Where(t => t.CountryId == IdCountry).ToList<ProvinceState>(); ;
        }

    }
}
