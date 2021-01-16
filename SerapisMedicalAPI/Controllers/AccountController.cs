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

            //return await _accountRepository.FacebookLogin(patient);
        }

        //[HttpGet]
        //public async Task<PatientUser> GetRegisteredUser(PatientUser patient)
        //{

        //    return await _accountRepository.FacebookLogin(patient);
        //}


        //POST: api/Account/
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Patient patient)
        {

            //Register the user

            if (!ModelState.IsValid)
                return BadRequest();

            await _accountRepository.SocialLogin(patient);
            
            return new OkObjectResult(patient);
        }
    }
}