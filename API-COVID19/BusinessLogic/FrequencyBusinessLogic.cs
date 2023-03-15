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

        public async Task SaveFrequencyDataToDB(List<Frequency> ListFrequency) 
        {
            try
            {
                _dbContext.Frequency.AddRange(ListFrequency);
                _dbContext.SaveChanges();

            }
            catch (Exception e)
            {

                throw e;
            }
        }


        private Dictionary<string, List<Frequency>> GetAVGCases(int CountryId) 
        {
            var DicAVGCases = new Dictionary<string, List<Frequency>>();

            var DailyAvgCasesCountry = _dbContext.Cases
                                .Where(t => t.CountryId == CountryId)
                                .GroupBy(c => c.DateReport)
                                .Select(g => new Frequency {
                                    DateReport = g.Key,
                                    Confirmed = g.Average(c => c.Confirmed),
                                    Deaths = g.Average(c => c.Deaths),
                                    Recovered = g.Average(c => c.Recovered),
                                    CountryId = CountryId,
                                    FrequencyTypeId = 1
                                })
                                .ToList();

            var MonthlyAvgCases = _dbContext.Cases
                                .Where(t => t.CountryId == CountryId)
                                .GroupBy(c => new {
                                    Month = c.DateReport.Month,
                                    Year = c.DateReport.Year
                                })
                                .Select(g => new Frequency {
                                    DateReport = new DateTime(g.Key.Year, g.Key.Month, 1).ToUniversalTime(),
                                    Confirmed = g.Average(c => c.Confirmed),
                                    Deaths = g.Average(c => c.Deaths),
                                    Recovered = g.Average(c => c.Recovered),
                                    CountryId = CountryId,
                                    FrequencyTypeId = 2,
                                })
                                .ToList();

            var YearlyAvgCases = _dbContext.Cases
                                .Where(t => t.CountryId == CountryId)
                                .GroupBy(c => c.DateReport.Year)
                                .Select(g => new Frequency {
                                    DateReport = new DateTime(g.Key, 1, 1).ToUniversalTime(),
                                    Confirmed = g.Average(c => c.Confirmed),
                                    Deaths = g.Average(c => c.Deaths),
                                    Recovered = g.Average(c => c.Recovered),
                                    CountryId = CountryId,
                                    FrequencyTypeId = 3,
                                })
                                .ToList();

            DicAVGCases.Add("Daily", DailyAvgCasesCountry);
            DicAVGCases.Add("Monthly", MonthlyAvgCases);
            DicAVGCases.Add("Yearly", YearlyAvgCases);


            return DicAVGCases;
        }


        public async Task<Dictionary<string, List<Frequency>>> FrecuencyByCountry()
        {
            var Countries = _dbContext.Country.ToList();
            var DicAVGCases = new Dictionary<string, List<Frequency>>();
            var ListFrequency = new List<Frequency>();

            foreach (var country in Countries) 
            {
                var AVGCases = GetAVGCases(country.Id);
                ListFrequency = AVGCases.Values.SelectMany(FrequencyList => FrequencyList).ToList();
                DicAVGCases.Add(country.Combined_Key, ListFrequency);
            }


            return DicAVGCases;

        }
    }
}
