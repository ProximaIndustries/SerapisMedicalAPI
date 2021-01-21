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

        // GET: api/Account
        [HttpGet]
        public async Task<IEnumerable<Patient>> GetAllRegisteredUser(Patient patient)
        {
            return await _accountRepository.GetAllRegisteredUsers();  //<-- this is for testing purporses
            
        }

        //[HttpGet]
        //public async Task<PatientUser> GetRegisteredUser(PatientUser patient)
        //{
        //
        //    return await _accountRepository.FacebookLogin(patient);
        //}


        //POST: api/Account/
        [HttpPost]
        public async Task<IActionResult> Test( [FromBody]Patient patient)   
        {

            //Register the user

            if (!ModelState.IsValid)
                return BadRequest();

            await _accountRepository.RegisterSocialUser(patient);
            
            return new OkObjectResult(patient);
        }

        //POST: api/Account?SocialID&Firstname&emailaddress
        [HttpPut]
        public async Task<IActionResult> Post([FromBody]Patient patient)
        {

            //Register the user

            if (!ModelState.IsValid)
                return BadRequest();

            await _accountRepository.SocialLogin(patient);
            
            //var patient = await _accountRepository.FBLogin(SocialID, FirstName, LastName, emailaddress);
           
            return new OkObjectResult(patient);
        }
    }
}