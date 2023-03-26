using API_COVID19.Models;

namespace API_COVID19.BusinessLogic
{
    public class FrequencyBusinessLogic
    {
        private ApplicationDbContext _dbContext;


        public FrequencyBusinessLogic(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<FrequencyType>> FrequencyTypesById(int IdType)
        {
            return _dbContext.FrequencyType.Where(t => t.Id == IdType).ToList();
        }

        public async Task<List<FrequencyType>> FrequencyTypes() 
        {
            return _dbContext.FrequencyType.ToList();
        }

        public async Task<List<Frequency>> GetFrecuencyByTypeFrecuency(int TypeFrecuency, int CountryId, DateTime? Date)
        {
            var DataFrecuency = _dbContext.Frequency
                                .Where(t => t.FrequencyTypeId == TypeFrecuency && t.CountryId == CountryId);

            if (Date.HasValue) 
            {

                Date = Date.Value.ToUniversalTime();

                switch (TypeFrecuency)
                {
                    case 1:
                        DataFrecuency = DataFrecuency.Where(t => t.DateReport.Date == Date);
                        break;
                    case 2:
                        DataFrecuency = DataFrecuency.Where(t => t.DateReport.Date.Year == Date.Value.Year && t.DateReport.Month == Date.Value.Month);
                        break;
                    case 3:
                        DataFrecuency = DataFrecuency.Where(t => t.DateReport.Date.Year == Date.Value.Year);
                        break;

                }

            }

            var ListFrecuency = DataFrecuency.ToList();

            return ListFrecuency;
        }

    }
}
