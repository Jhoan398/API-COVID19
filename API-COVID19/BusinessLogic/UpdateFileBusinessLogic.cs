using API_COVID19.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace API_COVID19.BusinessLogic
{
    public class UpdateFileBusinessLogic
    {
        private ApplicationDbContext _dbContext;

        public readonly string TypeCsvExt = ".csv";
        public readonly string RepoDataCovidUrl = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_daily_reports/";
        public readonly string RepoUSADataCovidUrl = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_daily_reports_us/";
        public readonly string RepoCountryStructureUrl = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/UID_ISO_FIPS_LookUp_Table";

        private HttpClient _HttpClient =  new HttpClient();

        public UpdateFileBusinessLogic(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public async Task SaveListDBData(List<DataCovid> DataCovid)
        {

            _dbContext.CountryDataCovid.AddRange(DataCovid);
            _dbContext.SaveChanges();
        }

        public List<DataCovid> GetListDataCovidUSA(string[] Content, string date) 
        {
            var ListDataCovid = new List<DataCovid>();
            var USACountry = _dbContext.Country.Include(t => t.ProvinceStates).Where(p => p.Id == 840).FirstOrDefault();
            var DateReport = DateTime.Parse(date);

            foreach (var line in Content.Skip(1))
            {
                // Province_State	Country_Region	Last_Update	Lat	Long_	Confirmed	Deaths	Recovered	Active	FIPS	Incident_Rate	Total_Test_Results	People_Hospitalized	Case_Fatality_Ratio	UID	ISO3	Testing_Rate	Hospitalization_Rate
                var values = line.Split(',');

                // Last Value is Empty
                if (String.IsNullOrEmpty(values[0]))
                    break;

                var Province_State = String.IsNullOrEmpty(values[0]) ? string.Empty : values[0];
                var Confirmed = String.IsNullOrEmpty(values[5]) ? 0 : decimal.Parse(values[5]);
                var Deaths = String.IsNullOrEmpty(values[6]) ? 0 : decimal.Parse(values[6]);
                var TotalCasesProvinces = Deaths + Confirmed;


                var ProvinceState = USACountry.ProvinceStates.Where(t => t.ProvinceName == Province_State).FirstOrDefault();
                if (ProvinceState != null)
                {
                    var Data = new DataCovid
                    {
                        CountryId = USACountry.Id,
                        Deads = Deaths,
                        Infected = Confirmed,
                        DateReport = DateTime.SpecifyKind(DateReport, DateTimeKind.Utc),
                        ProvinceStateId = ProvinceState.Id,
                        Total_Cases = TotalCasesProvinces
                    };

                    ListDataCovid.Add(Data);
                }


            }

            return ListDataCovid;

        }

        public List<DataCovid> GetListDataCovid(string[] Content, string date) 
        {
            try
            {
                var ListDataCovid = new List<DataCovid>();
                var ListCountries = _dbContext.Country.Include(t => t.ProvinceStates).ToList<Country>();
                var DateReport = DateTime.Parse(date);

                foreach (var line in Content.Skip(1))
                {
                    //FIPS,Admin2,Province_State,Country_Region,Last_Update,Lat,Long_,Confirmed,Deaths,Recovered,Active,Combined_Key,Incident_Rate,Case_Fatality_Ratio
                    var values = line.Split(',');
                    var FIPS = string.IsNullOrEmpty(values[0]) ? 0 : int.Parse(values[0]);

                    var Confirmed = string.IsNullOrEmpty(values[7])? 0 : decimal.Parse(values[7]);
                    var Deaths = string.IsNullOrEmpty(values[8]) ? 0 : decimal.Parse(values[8]);
                    var Province_State = string.IsNullOrEmpty(values[2]) ? string.Empty : values[2];
                    var Country_Region = values[3];

                    var Country = ListCountries.Where(t => t.Combined_Key == Country_Region).FirstOrDefault();
                    if (Country != null) 
                    {
                        var ListProvinceStates = Country.ProvinceStates;

                        if (FIPS != 0 && Country.Combined_Key == "US")
                            break;

                        var ProvinceState = new ProvinceState();
                        ProvinceState = ListProvinceStates.Where(t => t.ProvinceName == Province_State).FirstOrDefault();

                        var Data = new DataCovid
                        {
                            CountryId = Country.Id,
                            Deads = Deaths,
                            Infected = Confirmed,
                            DateReport = DateTime.SpecifyKind(DateReport, DateTimeKind.Utc),
                            ProvinceStateId = ProvinceState == null ? 0 : ProvinceState.Id,
                            Total_Cases = Deaths + Confirmed
                        };

                        ListDataCovid.Add(Data);
                    }
                    
                }

                return ListDataCovid;
            }
            catch (Exception e)
            {

                throw e;
            }
           
        }


        public async Task<List<Country>> GetCountriesStructure()
        {
            try
            {
                var urlFile = RepoCountryStructureUrl + TypeCsvExt;
                string[] lines = await GetStringCsvFile(urlFile);

                var Countries = new List<Country>();
                foreach (var line in lines.Skip(1))
                {
                    var values = line.Split(',');
                    var UID = int.Parse(values[0]);
                    var Code3 = string.IsNullOrEmpty(values[3]) ? UID : int.Parse(values[3]);
                    var FIPS = string.IsNullOrEmpty(values[4]) ? 0 : int.Parse(values[4]);

                    // Add Countries
                    var currentCountry = Countries.Find(x => x.Id == Code3);
                    if (currentCountry == null)
                    {
                        var strKeyCombined = string.IsNullOrEmpty(values[10]) ? string.Empty : (values[10]);
                        var countryRegion = string.IsNullOrEmpty(values[7]) ? string.Empty : values[7];


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
                        // Add Province States
                        var Province_Name = string.IsNullOrEmpty(values[6]) ? string.Empty : values[6];
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
                            var iso3 = string.IsNullOrEmpty(values[2]) ? string.Empty : values[2];
                            if (iso3.Equals("USA") && FIPS == 56)
                                break;
                        }
                    }
                }

                //240 registers
                return Countries;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string[]> GetStringCsvFile(string urlFile)
        {
            try
            {
                var response = await _HttpClient.GetAsync(urlFile);
                var fileContent = await response.Content.ReadAsStringAsync();
                string[] lines = fileContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                return lines;

            }
            catch (Exception)
            {
                string[] lines = new string[0];
                return lines;
            }

        }

    }
}
