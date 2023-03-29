using API_COVID19.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API_COVID19.BusinessLogic
{
    public class CasesBusinessLogic
    {
        private ApplicationDbContext _dbContext;

        public CasesBusinessLogic(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        private List<Cases> GetSumCasesByProvinceState(int CountryId, int ProvinceSId, DateTime InitialDate, DateTime FinalDate) 
        {
            List<Cases> cases = new List<Cases>();  
            var DataCases = _dbContext.Cases.Where(t => t.CountryId == CountryId && t.ProvinceStateId == ProvinceSId && t.DateReport.Date >= InitialDate && t.DateReport.Date <= FinalDate);

            var Case = new Cases
            {
                Confirmed = DataCases
                   .Sum(c => c.Confirmed),
                Deaths = DataCases
                   .Sum(c => c.Deaths),
                Recovered = DataCases
                   .Sum(c => c.Recovered),
                CountryId = CountryId,
                ProvinceStateId = ProvinceSId,
                DateReport = InitialDate
            };

            cases.Add(Case); 


            return cases;
        }

        private List<Cases> GetSumCasesByCountry(int CountryId, DateTime InitialDate, DateTime FinalDate) 
        {
            List<Cases> cases = new List<Cases>();
            var Case = new Cases();

            var DataCases = _dbContext.Cases.Where(c => c.CountryId == CountryId && c.DateReport.Date >= InitialDate && c.DateReport.Date <= FinalDate);

            Case = new Cases
            {
                Confirmed = DataCases
               .Sum(c => c.Confirmed),
                Deaths = DataCases
               .Sum(c => c.Deaths),
                Recovered = DataCases
               .Sum(c => c.Recovered),
                CountryId = CountryId,
                DateReport = InitialDate
            };

            cases.Add(Case);

            return cases;
        }

        public async Task<List<Cases>> GetSumCasesByConditions(int CountryId, int? ProvinceSId, DateTime InitialDate, DateTime FinalDate) 
        {

            if (ProvinceSId.HasValue)
                return GetSumCasesByProvinceState(CountryId, ProvinceSId.Value, InitialDate, FinalDate);

            return GetSumCasesByCountry(CountryId, InitialDate, FinalDate);

        }

        public async Task<List<Cases>> GetListCasesByDateReport(DateTime InitialDate) 
        {

            return _dbContext.Cases.Include(t => t.Country).Where(c => c.DateReport == InitialDate).Select(t => new Cases
            {
                Id = t.Id,
                CountryId = t.CountryId,
                DateReport = t.DateReport,
                Confirmed = t.Confirmed,
                Recovered = t.Recovered,
                Deaths = t.Deaths,       
                Country = t.Country

            }).ToList();
        }

        public async Task<List<Cases>> GetListCasesByConditions(int CountryId, int? ProvinceSId, DateTime InitialDate, DateTime FinalDate)
        {


            if (ProvinceSId.HasValue)
                return GetListCasesByProvinceState(CountryId, ProvinceSId.Value, InitialDate, FinalDate);

            return GetListCasesByCountry(CountryId, InitialDate, FinalDate);
        }


        private List<Cases> GetListCasesByCountry(int CountryId, DateTime InitialDate, DateTime FinalDate) 
        {

            return _dbContext.Cases.Where(c => c.CountryId == CountryId && c.DateReport.Date >= InitialDate && c.DateReport.Date <= FinalDate).OrderBy(t => t.DateReport).Select(t => new Cases
            {
                Id = t.Id,
                CountryId = t.CountryId,
                DateReport = t.DateReport,
                Confirmed = t.Confirmed,
                Recovered = t.Recovered,
                Deaths = t.Deaths
            
            }).ToList();

        }

        private List<Cases> GetListCasesByProvinceState(int CountryId, int ProvinceStateId,DateTime InitialDate, DateTime FinalDate)
        {
            return _dbContext.Cases.Where(t => t.CountryId == CountryId && t.ProvinceStateId == ProvinceStateId && t.DateReport.Date >= InitialDate && t.DateReport.Date <= FinalDate).OrderBy(t => t.DateReport).ToList(); 

        }

    }
}
