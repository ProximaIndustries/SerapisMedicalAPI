using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using SerapisMedicalAPI.Helpers;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.AppWrite;
using SerapisMedicalAPI.Model.PatientModel;
using SerapisMedicalAPI.Model.Supabase;
using SerapisMedicalAPI.Services.SymptomsChecker;
using Serilog;

namespace SerapisMedicalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountSupabaseRepository _supabaseRepository;

        public AuthController(IAccountSupabaseRepository supabaseRepository, IAccountRepository accountRepository)
        {
            _supabaseRepository = supabaseRepository;
            _accountRepository = accountRepository;
        }

        [HttpPost("supabase/register")]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(BaseResponse<Patient>))]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int) HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RegisterUser([FromBody] Patient patient)
        {
            Log.Information($"Request:{patient?.ToJson()}");
            var response = await _supabaseRepository.RegisterUser(patient);

            return new OkObjectResult(response);
        }

        [HttpPost("supabase/login/{authId}")]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(Task<BaseResponse<PatientAuthResponse<Patient>>>))]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int) HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> LoginUser([FromBody] string authId)
        {
            Log.Information("Request:{request}", authId);
            var response = await _accountRepository.GetPractionerById(authId);
            var encrypt = EncryptService.EncryptPlainTextToCipherText(response.ToJson());
            var decrypt = EncryptService.DecryptCipherTextToPlainText(encrypt);
            Log.Information(encrypt);
            Log.Information(decrypt);
            Log.Information($"Request:{response?.ToJson()}");
            return new OkObjectResult(response);
        }

        [HttpPost("sso/register")]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(Task<BaseResponse<PatientAuthResponse<Doctor>>>))]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int) HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RegisterSSOUser([FromBody] AppWriteAuthRequest user)
        {
            Log.Information($"Request:{user?.ToJson()}");

            var response = await _supabaseRepository.LoginUser(null);

            Log.Information($"Request:{response?.ToJson()}");
            return new OkObjectResult(response);
        }

        [HttpPost("sso/login")]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(AppWriteSession))]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int) HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> LoginSSOUser([FromBody] SupabaseAuth user)
        {
            //var response  = await _appWriteRepository.LoginUser(user);
            Log.Information($"Request:{user?.ToJson()}");

            var response = await _supabaseRepository.LoginSSOUser(user);

            Log.Information($"Request:{response?.ToJson()}");
            return new OkObjectResult(response);
        }

        [HttpPut("sso/linkUserWithPractice")]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(Task<BaseResponse<PatientAuthResponse<Doctor>>>))]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int) HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> LinkSSOUserWithPractice([FromBody] AppWriteAuthRequest user)
        {
            Log.Information($"Request:{user?.ToJson()}");

            var response = await _supabaseRepository.LoginUser(null);

            Log.Information($"Request:{response?.ToJson()}");
            return new OkObjectResult(response);
        }

        [HttpPut("sso/deLinkUserWithPractice")]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(Task<BaseResponse<PatientAuthResponse<Doctor>>>))]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int) HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> DeLinkSSOUserWithPractice([FromBody] SupabaseAuth user)
        {
            Log.Information($"Request:{user?.ToJson()}");

            var response = await _supabaseRepository.LoginUser(user);

            Log.Information($"Request:{response?.ToJson()}");
            return new OkObjectResult(response);
        }
    }
}