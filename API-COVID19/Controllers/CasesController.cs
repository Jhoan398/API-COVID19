﻿using API_COVID19.BusinessLogic;
using API_COVID19.Models;
using Microsoft.AspNetCore.Mvc;
using static API_COVID19.Models.ApiResponse;

namespace API_COVID19.Controllers
{
    [ApiController]
    [Route("api/Cases")]
    public class CasesController : Controller
    {
        private readonly CasesBusinessLogic _db;

        public CasesController(ApplicationDbContext AppDataContext)
        {
            _db = new CasesBusinessLogic(AppDataContext);
        }

        [HttpGet]
        [Route("CountryCasesByConditions")]
        public async Task<IActionResult> CountryCasesByConditions(int CountryId, int? PronvinceSId , DateTime InitialDate, DateTime FinalDate)
        {

            try
            {
               
                InitialDate = InitialDate.Date.ToUniversalTime();
                FinalDate = FinalDate.Date.ToUniversalTime();

                var cases = await _db.GetListCasesByConditions(CountryId, PronvinceSId, InitialDate, FinalDate);

                //if (IsSumattion.Value) 
                //{
                //    cases = await _db.GetSumCasesByConditions(CountryId, PronvinceSId, InitialDate, FinalDate);
                //}
                //else 
                //{
                //    cases = await _db.GetListCasesByConditions(CountryId, PronvinceSId, InitialDate, FinalDate);
                //}



                return Ok(cases);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }

        }

        [HttpGet]
        [Route("CountryCasesByDateReport")]
        public async Task<IActionResult> CountryCasesByDateReport(DateTime InitialDate)
        {

            try
            {

                InitialDate = InitialDate.Date.ToUniversalTime();

                var cases = await _db.GetListCasesByDateReport(InitialDate);


                return Ok(cases);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }

        }

    }
}
