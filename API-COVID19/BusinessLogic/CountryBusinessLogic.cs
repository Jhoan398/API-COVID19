using API_COVID19.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace API_COVID19.BusinessLogic
{
    public class CountryBusinessLogic
    {

        private ApplicationDbContext _dbContext;

        public CountryBusinessLogic(ApplicationDbContext dbContext)
        {
            _dbContext= dbContext;  
        }


        public async void SaveDBData(List<Country> countries) 
        {

            try
            {           

                _dbContext.Country.AddRange(countries);
                _dbContext.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
          
        }

        public async Task<List<Country>> GetCountriesFromCsvTable() 
        {
            try
            {
                var urlFile = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/UID_ISO_FIPS_LookUp_Table.csv";
                List<Country> Countries = new List<Country>();

                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(urlFile))
                    {
                        response.EnsureSuccessStatusCode();
                        var fileContent = await response.Content.ReadAsStringAsync();
                        string[] lines = fileContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                        foreach (var line in lines.Skip(1))
                        {

                            var values = line.Split(',');
                            var UID = int.Parse(values[0]);
                            var Code3 = string.IsNullOrEmpty(values[3]) ? UID : int.Parse(values[3]);
                            var FIPS = string.IsNullOrEmpty(values[4]) ? 0 : int.Parse(values[4]);

                            var currentCountry = Countries.Find(x => x.Id == Code3);
                            if (currentCountry == null)
                            {
                                var strKeyCombined = string.IsNullOrEmpty(values[10]) ? string.Empty : (values[10]);
                                var countryRegion = String.IsNullOrEmpty(values[7]) ? String.Empty : values[7];


                                if (strKeyCombined.Contains("\""))
                                    strKeyCombined = values[6] + ", " + countryRegion;

                                var country = new Country
                                {
                                    Id = UID,
                                    Country_Name = countryRegion,
                                    Combined_Key = strKeyCombined

                                };

                                Countries.Add(country);
                            }
                            else
                            {
                                var Province_Name = String.IsNullOrEmpty(values[6]) ? String.Empty : values[6];
                                var ProvinceState = new ProvinceState
                                {
                                    Id = UID,
                                    CountryId = currentCountry.Id,
                                    ProvinceName = Province_Name,
                                    Country = currentCountry,
                                };

                                currentCountry.ProvinceStates.Add(ProvinceState);


                                //TAKE 56 states of -USA
                                if (Code3 == 840)
                                {
                                    var iso3 = String.IsNullOrEmpty(values[2]) ? String.Empty : values[2];
                                    if (iso3.Equals("USA") && FIPS == 56)
                                        break;
                                }
                            }
                        }
                    }


                }


                return Countries;
            }
            catch (Exception)
            {

                throw;
            }
        } 


        public async Task<string[]> GetCsvTable() 
        {
            try
            {
                var urlFile = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/UID_ISO_FIPS_LookUp_Table.csv";
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(urlFile))
                    {
                        response.EnsureSuccessStatusCode();
                        var fileContent = await response.Content.ReadAsStringAsync();
                        string[] lines = fileContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                        return lines;
                    }
                }

            }
            catch (Exception)
            {
                string[] lines = new string[0];
                return lines;
            }

        }

    }
}
