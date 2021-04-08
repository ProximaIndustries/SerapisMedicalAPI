using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using System.Collections.Generic;
using SerapisMedicalAPI.Model.PatientModel;

namespace SerapisMedicalAPI.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        //api/account?socialid=MP0703150&firstname=bonga&lastname=ngcobo    
        [HttpGet]
        public async Task<IActionResult> AutenticateSocialUser(string socialid, string firstname, string lastname)
        {

            Patient _patient = await _accountRepository.SocialLogin(socialid, firstname, lastname);
            if (_patient == null)
                return BadRequest(_patient);


            /*PatientDao patientDao = new PatientDao
            {
                id = _patient.id.ToString(),
                SocialID = _patient.SocialID,
                Email = _patient.Email,
                PatientLastName = _patient.PatientLastName
                PatientMedicalAid
                MedicalAidPatient
                PatientAge
            };*/
            //return new OkObjectResult(_patient);
            //response.ErrorMessage = "There was an internal error, please contact to technical support."
            // Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetStockItemAsync), ex);
            return new OkObjectResult(_patient);
        }

        //POST: api/Account?SocialID&Firstname&emailaddress
        [HttpPut]
        public async Task<IActionResult> Post([FromBody]Patient patient)
        {

            //Register the user

            if (!ModelState.IsValid)
                return BadRequest();

            //await _accountRepository.SocialLogin(patient);
            
            //var patient = await _accountRepository.FBLogin(SocialID, FirstName, LastName, emailaddress);
           
            return new OkObjectResult(patient);
        }



    }
}