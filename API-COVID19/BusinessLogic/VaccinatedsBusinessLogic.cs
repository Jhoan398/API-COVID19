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

        public async Task<List<Vaccinateds>> GetVaccinatedsByDateReport(int CountryId, DateTime InitialDate, DateTime FinalDate)
        {

            return _dbContext.Vaccinateds.Where(t => t.CountryId == CountryId && t.DateReport >= InitialDate && t.DateReport <= FinalDate).ToList();

        }


    }
}
