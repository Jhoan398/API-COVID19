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


        public List<Cases> GetCasesByCountryName(string CountryName) 
        {

            var Country = _dbContext.Country.Where(t => t.Combined_Key == CountryName).FirstOrDefault();

            return _dbContext.Cases.Where(t => t.CountryId == Country.Id).ToList<Cases>();

        }


        public async Task<List<Cases>> GetCasesByCountryId(int CountryId)
        {
            return _dbContext.Cases.Where(t => t.CountryId == CountryId).ToList<Cases>();
        }

        public async Task<List<Cases>> GetCasesByCountry_PronvinceName(string CountryName, string PronvinceSName) 
        {
            var Country = _dbContext.Country.Include(t => t.ProvinceStates).Where(p => p.Combined_Key == CountryName).FirstOrDefault();
            var PronvinceState = Country.ProvinceStates.Where(t => t.ProvinceName == PronvinceSName).FirstOrDefault();


            return _dbContext.Cases.Where(t => t.ProvinceStateId == PronvinceState.Id).ToList();
        }
        public async Task<Cases> GetCasesBeetwenDatesByCountryId(DateTime InitialDate, DateTime FinalDate, int CountryId) 
        {
            //MM-dd-YYYY 
            var sumaCasosEnRango = new Cases
            {
                Confirmed = _dbContext.Cases
                   .Where(c => c.DateReport >= InitialDate && c.DateReport <= FinalDate && c.CountryId == CountryId)
                   .Sum(c => c.Confirmed),
                Deaths = _dbContext.Cases
                   .Where(c => c.DateReport >= InitialDate && c.DateReport <= FinalDate && c.CountryId == CountryId)
                   .Sum(c => c.Deaths),
                Recovered = _dbContext.Cases
                   .Where(c => c.DateReport >= InitialDate && c.DateReport <= FinalDate && c.CountryId == CountryId)
                   .Sum(c => c.Recovered),
                CountryId = CountryId
            };


            return sumaCasosEnRango;
        }

        public async Task<List<Cases>> GetCasesByCountry_PronvinceName(int CountryId, int PronvinceSId)
        {
            var Country = _dbContext.Country.Include(t => t.ProvinceStates).Where(p => p.Id == CountryId).FirstOrDefault();
            var PronvinceState = Country.ProvinceStates.Where(t => t.Id == PronvinceSId).FirstOrDefault();


            return _dbContext.Cases.Where(t => t.ProvinceStateId == PronvinceState.Id).ToList();
        }

        public async Task<List<Cases>> GetCasesByCountry(int CountryId, DateTime InitialDate, DateTime? FinalDate) 
        {
            var listCases = new List<Cases>();
            var Case = new Cases();

            if (FinalDate.HasValue)
            {
                FinalDate = FinalDate.Value.ToUniversalTime();

                Case = new Cases
                {
                    Confirmed = _dbContext.Cases
                   .Where(c => c.DateReport >= InitialDate && c.DateReport <= FinalDate && c.CountryId == CountryId)
                   .Sum(c => c.Confirmed),
                    Deaths = _dbContext.Cases
                   .Where(c => c.DateReport >= InitialDate && c.DateReport <= FinalDate && c.CountryId == CountryId)
                   .Sum(c => c.Deaths),
                    Recovered = _dbContext.Cases
                   .Where(c => c.DateReport >= InitialDate && c.DateReport <= FinalDate && c.CountryId == CountryId)
                   .Sum(c => c.Recovered),
                    CountryId = CountryId
                };
            }
            else 
            {
                var DataCases = _dbContext.Cases.Where(t => t.CountryId == CountryId && t.DateReport == InitialDate.ToUniversalTime());
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

    }
}
