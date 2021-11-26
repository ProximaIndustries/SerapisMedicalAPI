using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using System.Collections.Generic;
using SerapisMedicalAPI.Model.PatientModel;
using Microsoft.Extensions.Logging;

namespace SerapisMedicalAPI.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAccountRepository accountRepository, ILogger<AccountController> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;

        }

        //api/account?socialid=MP0703150&firstname=bonga&lastname=ngcobo    
        [HttpGet]
        public async Task<IActionResult> AutenticateSocialUser(string socialid, string firstname, string lastname)
        {

            Patient _patient = await _accountRepository.SocialLogin(socialid, firstname, lastname);
            if (_patient == null)
                return BadRequest(_patient);

            //response.ErrorMessage = "There was an internal error, please contact to technical support."
            _logger?.LogCritical("There was an error on '{0}' invocation: {1}");
            return new OkObjectResult(_patient);
        }

        //POST: api/Account?SocialID&Firstname&emailaddress
        [HttpPut]
        public async Task<IActionResult> Post([FromBody]Patient patient)
            {

            //Register the user

            if (!ModelState.IsValid)
                
                return BadRequest();

            _ = await _accountRepository.SocialLogin(null,patient.PatientFirstName,patient.PatientLastName);
            
            //var patient = await _accountRepository.FBLogin(SocialID, FirstName, LastName, emailaddress);
           
            return new OkObjectResult(patient);
        }


            
    }
}