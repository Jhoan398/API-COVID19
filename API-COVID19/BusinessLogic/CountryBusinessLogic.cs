using API_COVID19.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq;

namespace API_COVID19.BusinessLogic
{
    public class CountryBusinessLogic
    {

        private ApplicationDbContext _dbContext;

        public CountryBusinessLogic(ApplicationDbContext dbContext)
        {
            _dbContext= dbContext;  
        }

        public async Task<Country> GetCountryById(int UID)
        {
            return _dbContext.Country.Where(t => t.Id == UID).FirstOrDefault();
        }

        public async Task<Country> GetCountryByNameAsync(string CountryName)
        {
            return _dbContext.Country.Where(t => t.Combined_Key.Contains(CountryName)).FirstOrDefault();
        }


        public async Task<List<Country>> GetCountries()
        {
            return _dbContext.Country.ToList<Country>();
        }

    }
}
