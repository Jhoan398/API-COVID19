using API_COVID19.Controllers;
using API_COVID19.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;

namespace API_COVID19.BusinessLogic
{
    public class UpdateFileBusinessLogic
    {
        private ApplicationDbContext _dbContext;

        private HttpClient _HttpClient =  new HttpClient();

        public UpdateFileBusinessLogic(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async void SaveCountriesStructureToDB(List<Country> countries)
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

        public async Task SaveCountriesCasesToDB(List<Cases> ListCasesCovid)
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
        public async Task SaveCountriesVaccinatedToDB(List<Vaccinateds> ListVaccinateds)
        {
            try
            {
                _dbContext.Vaccinateds.AddRange(ListVaccinateds);
                _dbContext.SaveChanges();

            }
            catch (Exception e)
            {

                throw e;
            }

        }



        public async Task<Dictionary<string, List<Cases>>> GetWorldWideCases(DateTime dateReport) 
        {
            var TypeCsvExt = ".csv";
            var RepoDataCovidUrl = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_daily_reports/";
            var RepoUSADataCovidUrl = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_daily_reports_us/";
            var FileName = RepoDataCovidUrl + dateReport.ToString("MM-dd-yyyy") + TypeCsvExt;
            var FileNameUSA = RepoUSADataCovidUrl + dateReport.ToString("MM-dd-yyyy") + TypeCsvExt;


            string[] ContentUSA = await GetStringCsvFile(FileNameUSA);
            var USACases = GetListCasesCovidUSA(ContentUSA, dateReport);

            string[] Content = await GetStringCsvFile(FileName);
            var WorldCases = GetListWorldCasesCovid(Content, dateReport);

            //if (!WorldCases.Any() || !USACases.Any())

            var dicCases = new Dictionary<string, List<Cases>>
            {
                { "World" , WorldCases },
                { "USA" , USACases }
            };


            return dicCases;
        }



        //Permite el mapeo de los datos para los casos de estados unidos
        private List<Cases> GetListCasesCovidUSA(string[] Content, DateTime DateReport)
        {
            var ListCasesCovid = new List<Cases>();
            var USACountry = _dbContext.Country.Include(t => t.ProvinceStates).Where(p => p.Id == 840).FirstOrDefault();

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
                        DateReport = DateReport.ToUniversalTime(),
                        ProvinceStateId = ProvinceState.Id,
                    };


                    ListCasesCovid.Add(Cases);
                }


            }

            return ListCasesCovid;

        }

        public async Task<List<Vaccinateds>> GetListVaccinateds()
        {
            try
            {
                //Date,UID,Province_State,Country_Region,Doses_admin,People_at_least_one_dose
                var FileNameVaccinateds = "https://raw.githubusercontent.com/govex/COVID-19/master/data_tables/vaccine_data/global_data/time_series_covid19_vaccine_global.csv";
                var ListVaccinateds = new List<Vaccinateds>();
                string[] Content = await GetStringCsvFile(FileNameVaccinateds);
                var Countries = _dbContext.Country.ToList();

                foreach (var line in Content.Skip(1))
                {
                    var values = line.Split(',');
                    
                    if (string.IsNullOrEmpty(values[0]))
                        break;
                    
                    var Date = string.IsNullOrEmpty(values[0]) ? string.Empty : values[0];
                    var UID = string.IsNullOrEmpty(values[1]) ? 1 : int.Parse(values[1]);
                    var Doses_admin = new decimal();
                    var People_at_least_one_dose = new decimal();
                    var DateReport = DateTime.Parse(Date).Date.ToUniversalTime();

                    if (values.Length == 6)
                    {
                        Doses_admin = string.IsNullOrEmpty(values[4]) ? 0 : decimal.Parse(values[4]);
                        People_at_least_one_dose = string.IsNullOrEmpty(values[5]) ? 0 : decimal.Parse(values[5]);
                    }
                    else if (values.Length == 7)
                    {
                        Doses_admin = string.IsNullOrEmpty(values[5]) ? 0 : decimal.Parse(values[5]); // "Korea, South"
                        People_at_least_one_dose = string.IsNullOrEmpty(values[6]) ? 0 : decimal.Parse(values[6]);
                    } 
                    
                    var currentCountry = Countries.Where(t => t.Id == UID).FirstOrDefault();
                    
                    if (currentCountry != null)
                    {
                        var DataVaccinated = new Vaccinateds
                        {
                            CountryId = UID,
                            AtLeastOneDosis = People_at_least_one_dose,
                            Dosis = Doses_admin,
                            DateReport = DateReport
                        };

                        ListVaccinateds.Add(DataVaccinated);
                    }


                }



                return ListVaccinateds;
            }
            catch (Exception e)
            {

                throw e;
            }
           
        }

        //Permite el mapeo de los datos para los casos de todos los paises
        private List<Cases> GetListWorldCasesCovid(string[] Content, DateTime DateReport)
        {
            try
            {
                var ListCasesCovid = new List<Cases>();
                var ListCountries = _dbContext.Country.Include(t => t.ProvinceStates).ToList<Country>();

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
                            DateReport = DateReport.ToUniversalTime(),
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


        // Permite la inserción de los paises y departamentos('Estados')
        public async Task<List<Country>> GetCountriesStructure()
        {
            try
            {
                //UID	iso2	iso3	code3	FIPS	Admin2	Province_State	Country_Region	Lat	Long_	Combined_Key	Population
                var urlFile = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/UID_ISO_FIPS_LookUp_Table.csv";
                string[] lines = await GetStringCsvFile(urlFile);

                var Countries = new List<Country>();
                foreach (var line in lines.Skip(1))
                {
                    var values = line.Split(',');
                    var UID = int.Parse(values[0]);
                    var Code3 = string.IsNullOrEmpty(values[3]) ? UID : int.Parse(values[3]);
                    var FIPS = string.IsNullOrEmpty(values[4]) ? 0 : int.Parse(values[4]);
                    var provinceState = string.IsNullOrEmpty(values[6]) ? string.Empty : values[6];
                    var countryRegion = string.IsNullOrEmpty(values[7]) ? string.Empty : values[7];
                    var strKeyCombined = string.Empty;
                    var Population = new decimal();

                    if (values.Length == 12)
                    {
                        strKeyCombined = countryRegion;
                        Population = string.IsNullOrEmpty(values[11]) ? 0 : decimal.Parse(values[11]);
                    }
                    else if (values.Length == 13)
                    {
                        strKeyCombined = provinceState + ", " + countryRegion;
                        Population = string.IsNullOrEmpty(values[12]) ? 0 : decimal.Parse(values[12]);
                    }
                    else if (values.Length == 14)
                    {
                        provinceState = string.IsNullOrEmpty(values[8]) ? string.Empty : values[8];
                        strKeyCombined = countryRegion + ", " + provinceState; //"Korea, South"
                        countryRegion = strKeyCombined;
                        Population = string.IsNullOrEmpty(values[13]) ? 0 : decimal.Parse(values[13]);
                    }
                    else 
                    {
                        provinceState = provinceState + ", " + countryRegion;
                        countryRegion = string.IsNullOrEmpty(values[8]) ? string.Empty : values[8];
                        strKeyCombined = provinceState + ", " +countryRegion; //"Bonaire,  Sint Eustatius and Saba", Netherlands
                        Population = string.IsNullOrEmpty(values[14]) ? 0 : decimal.Parse(values[14]);
                    }

                    // Add Countries
                    var currentCountry = Countries.Find(x => x.Id == Code3);
                    if (currentCountry == null)
                    {

                        var country = new Country
                        {
                            Id = UID,
                            Country_Name = countryRegion,
                            Combined_Key = strKeyCombined,
                            Populate = Population
                        };

                        Countries.Add(country);
                    }
                    else
                    {
                        // Add Province States
                        var ProvinceState = new ProvinceState
                        {
                            Id = UID,
                            CountryId = currentCountry.Id,
                            ProvinceName = provinceState,
                            Country = currentCountry,
                            Populate = Population
                        };

                        currentCountry.ProvinceStates.Add(ProvinceState);


                        //TAKE 56 states of -USA
                        if (Code3 == 840)
                        {
                            var iso3 = string.IsNullOrEmpty(values[2]) ? string.Empty : values[2];

                            // Termino de tomar los estados de USA y termino agregando el ultimo de registro del "mundo"
                            if (iso3.Equals("USA") && FIPS == 56)
                            {
                                var country = new Country
                                {
                                    Id = 1,
                                    Country_Name = "World",
                                    Combined_Key = "World",
                                };
     
                                Countries.Add(country);
                                break;
                            }

                        }
                    }
                }

                //241 registers
                return Countries;
            }
            catch (Exception e)
            {

                throw e;
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
