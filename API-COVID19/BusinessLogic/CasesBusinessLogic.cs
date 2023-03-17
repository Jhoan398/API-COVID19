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


        private List<Cases> GetCasesByProvinceState(int CountryId, int ProvinceSId, DateTime InitialDate, DateTime? FinalDate) 
        {
            var listCases = new List<Cases>();
            var Case = new Cases();
            InitialDate = InitialDate.ToUniversalTime();
            var DataCases = _dbContext.Cases.Where(t => t.CountryId == CountryId && t.ProvinceStateId == ProvinceSId);

            if (FinalDate.HasValue)
            {
                FinalDate = FinalDate.Value.ToUniversalTime();
                DataCases = DataCases.Where(c => c.DateReport >= InitialDate && c.DateReport <= FinalDate);

                Case = new Cases
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
            }
            else 
            {
                Case = DataCases.Where(c => c.DateReport == InitialDate).FirstOrDefault();
            }


            listCases.Add(Case);

            return listCases;
        }

        private List<Cases> GetCasesByCountry(int CountryId, DateTime InitialDate, DateTime? FinalDate) 
        {
            var listCases = new List<Cases>();
            var Case = new Cases();
            var DataCases = _dbContext.Cases.Where(t => t.CountryId == CountryId);
            InitialDate = InitialDate.ToUniversalTime();

            if (FinalDate.HasValue)
            {
                FinalDate = FinalDate.Value.ToUniversalTime();
                DataCases = DataCases.Where(c => c.DateReport >= InitialDate && c.DateReport <= FinalDate);

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
            }
            else
            {
                DataCases = DataCases.Where(t => t.DateReport == InitialDate);

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
            }

            listCases.Add(Case);

            return listCases;
        }

        public async Task<List<Cases>> GetCasesByConditions(int CountryId, int? ProvinceSId, DateTime InitialDate, DateTime? FinalDate) 
        {

            if (ProvinceSId.HasValue)
                return GetCasesByProvinceState(CountryId, ProvinceSId.Value, InitialDate, FinalDate);

            return GetCasesByCountry(CountryId, InitialDate, FinalDate);

        }  

    }
}
