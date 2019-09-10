using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;

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


        //POST:api/Account/post
        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> Post([FromBody]PatientUser user)
        {
            
            //await _accountRepository.AddAccount(new PatientUser
            //{
            //    Email = user.Email,
            //    PrivateId= user.PrivateId,
            //    Token = user.Token,
            //    Password= user.Password,
            //    FirstName= user.FirstName,
            //    Surname = user.Surname,
            //    Age= user.Age
                
            //});
            
            await _accountRepository.AddAccount(user);
            return new OkObjectResult(user);

        }

        //POST:api/Account/
        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> FacebookLogin([FromBody] PatientUser user)
        {

            await _accountRepository.AddAccount(user);
            return new OkObjectResult(user);
        }
    }
}