using API_COVID19.Controllers;
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

        public async Task SaveCasesToDB(List<Cases> ListCasesCovid)
        {
            try
            {
                _dbContext.Cases.AddRange(ListCasesCovid);
                _dbContext.SaveChanges();

            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        //public async void CreateReportCases(string date) 
        //{
        //    var ListCountries = _dbContext.Country.Include(t => t.ProvinceStates).ToList();
        //    var ListCountryReports = new List<CountryCaseReport>();
        //    var ListProvinceReports = new List<ProvinceStateCaseReport>();
        //    var DateReport = DateTime.Parse(date).Date.ToUniversalTime();

        //    foreach (var Country in ListCountries)
        //    {
        //        var Country_ProvinceStates = Country.ProvinceStates;
        //        if (Country_ProvinceStates.Count > 0)
        //        {
        //            foreach (var ProvinceState in Country_ProvinceStates)
        //            {
        //                var ProvinceStateCase = new ProvinceStateCaseReport
        //                {
        //                    ProvinceStateId = ProvinceState.Id,
        //                    DateReport = DateReport,
        //                };

        //                ListProvinceReports.Add(ProvinceStateCase);

        //            }
        //        }

        //        var CountryCase = new CountryCaseReport
        //        {
        //            CountryId = Country.Id,
        //            DateReport = DateReport
        //        };

        //        ListCountryReports.Add(CountryCase);
        //    }

        //     _dbContext.CountryCaseReport.AddRange(ListCountryReports);
        //     _dbContext.ProvinceStateCaseReport.AddRange(ListProvinceReports);
        //     _dbContext.SaveChanges();
        //}

        public List<Cases> GetListCasesCovidUSA(string[] Content, string date)
        {
            var ListCasesCovid = new List<Cases>();
            var USACountry = _dbContext.Country.Include(t => t.ProvinceStates).Where(p => p.Id == 840).FirstOrDefault();
            var DateReport = DateTime.Parse(date).Date.ToUniversalTime();

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
                var Recovered = string.IsNullOrEmpty(values[7]) ? 0 : decimal.Parse(values[7]);
                var Active = string.IsNullOrEmpty(values[8]) ? 0 : decimal.Parse(values[8]);

                var ProvinceState = USACountry.ProvinceStates.Where(t => t.ProvinceName == Province_State).FirstOrDefault();

                if (ProvinceState != null)
                {
                    var Cases = new Cases
                    {
                        CountryId = USACountry.Id,
                        Deaths = Deaths,
                        Confirmed = Confirmed,
                        Recovered = Recovered,
                        DateReport = DateReport,
                        ProvinceStateId = ProvinceState.Id,
                    };


                    ListCasesCovid.Add(Cases);
                }


            }

            return ListCasesCovid;

        }

        public List<Cases> GetListDataCovid(string[] Content, string date)
        {
            try
            {
                var ListCasesCovid = new List<Cases>();
                var ListCountries = _dbContext.Country.Include(t => t.ProvinceStates).ToList<Country>();
                var DateReport = DateTime.Parse(date).Date.ToUniversalTime();

                foreach (var line in Content.Skip(1))
                {
                    //FIPS,Admin2,Province_State,Country_Region,Last_Update,Lat,Long_,Confirmed,Deaths,Recovered,Active,Combined_Key,Incident_Rate,Case_Fatality_Ratio
                    var values = line.Split(',');
                    var FIPS = string.IsNullOrEmpty(values[0]) ? 0 : int.Parse(values[0]);
                    var Province_State = string.IsNullOrEmpty(values[2]) ? string.Empty : values[2];
                    var Country_Region = string.IsNullOrEmpty(values[3]) ? string.Empty : values[3];
                    var Confirmed = string.IsNullOrEmpty(values[7]) ? 0 : decimal.Parse(values[7]);
                    var Deaths = string.IsNullOrEmpty(values[8]) ? 0 : decimal.Parse(values[8]);
                    var Recovered = string.IsNullOrEmpty(values[9]) ? 0 : decimal.Parse(values[9]);
                    var Active = string.IsNullOrEmpty(values[9]) ? 0 : decimal.Parse(values[9]);

                    var Country = ListCountries.Where(t => t.Combined_Key == Country_Region).FirstOrDefault();
                    if (Country != null)
                    {
                        if (FIPS != 0 && Country.Combined_Key == "US")
                            break;

                        var ListProvinceStates = Country.ProvinceStates;

                        var ProvinceState = new ProvinceState();
                        ProvinceState = ListProvinceStates.Where(t => t.ProvinceName == Province_State).FirstOrDefault();

                        var Data = new Cases
                        {
                            CountryId = Country.Id,
                            Deaths = Deaths,
                            Confirmed = Confirmed,
                            Recovered = Recovered,
                            DateReport = DateReport,
                            ProvinceStateId = ProvinceState != null ? ProvinceState.Id : null,
                        };

                        ListCasesCovid.Add(Data);
                    }

                }

                return ListCasesCovid;
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
