using API_COVID19.Models;

namespace API_COVID19.BusinessLogic
{
    public class VaccinatedsBusinessLogic
    {
        private ApplicationDbContext _dbContext;

        public VaccinatedsBusinessLogic(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Vaccinateds> GetVaccinatedsByDateReport(int CountryId, DateTime InitialDate) 
        {

            return _dbContext.Vaccinateds.Where(t => t.CountryId == CountryId && t.DateReport == InitialDate.ToUniversalTime()).FirstOrDefault();
            
        }


    }
}
