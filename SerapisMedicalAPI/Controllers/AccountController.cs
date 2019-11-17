using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using System.Collections.Generic;

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
        public async Task<IEnumerable<PatientUser>> Get()
        {
            return await _accountRepository.GetAllRegisteredUsers();
        }

        //POST: api/Account/
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PatientUser patient)
        {

            //Find if user exists first
            //if exists return profile info
            //else create new user

            if (!ModelState.IsValid)
                return BadRequest();
            
            await _accountRepository.RegisterSocialUser(new PatientUser
            {
                Email = patient.Email,
                FirstName = patient.FirstName,
                Surname = patient.Surname,
                Age = patient.Age       
            });

            //await _accountRepository.RegisterSocialUser(patient);
            return new OkObjectResult(patient);
        }
    }
}