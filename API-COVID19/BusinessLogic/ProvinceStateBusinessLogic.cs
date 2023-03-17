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

        public async Task<List<ProvinceState>> GetProvinceStatesFromCountry(int IdCountry)
        {
            return _dbContext.ProvinceState.Where(t => t.CountryId == IdCountry).ToList<ProvinceState>();
        }

        public async Task<List<ProvinceState>> GetProvinceStatesById(int IdProvinceState) 
        {
            return _dbContext.ProvinceState.Where(t => t.Id == IdProvinceState).ToList<ProvinceState>();
        }

        public async Task<List<ProvinceState>> GetProvinceStatesByName(string NameProvinceState)
        {
            return _dbContext.ProvinceState.Where(t => t.ProvinceName.Contains(NameProvinceState)).ToList<ProvinceState>();
        }

    }
}
