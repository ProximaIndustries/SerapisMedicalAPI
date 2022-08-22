using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model.PatientModel;

namespace SerapisMedicalAPI.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountAppwriteRepository _appWriteRepository;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountRepository accountRepository, ILogger<AccountController> logger,
            IAccountAppwriteRepository appWriteRepository)
        {
            _accountRepository = accountRepository;
            _logger = logger;
            _appWriteRepository = appWriteRepository;
        }
        //api/
        /*public async Task<IActionResult> GetAccount(object req)
        {
            var _patient =  new object();
            if (_patient == null)
                return BadRequest(_patient);

            //response.ErrorMessage = "There was an internal error, please contact to technical support."
            _logger?.LogCritical("There was an error on '{0}' invocation: {1}");
            return new OkObjectResult(_patient);
        }*/

        //POST: api/Account?SocialID&Firstname&emailaddress
        [HttpPut]
        public async Task<IActionResult> Post([FromBody] Patient patient)
        {
            //Register the user

            if (!ModelState.IsValid)

                return BadRequest();

            _ = await _accountRepository.SocialLogin(null, patient.PatientFirstName, patient.PatientLastName);

            //var patient = await _accountRepository.FBLogin(SocialID, FirstName, LastName, emailaddress);

            return new OkObjectResult(patient);
        }
    }
}