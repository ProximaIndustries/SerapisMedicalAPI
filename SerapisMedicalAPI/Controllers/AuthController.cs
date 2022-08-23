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
        private readonly IDoctorRepository _doctorRepository;

        public AuthController(IAccountSupabaseRepository supabaseRepository, IAccountRepository accountRepository, IDoctorRepository doctorRepository)
        {
            _supabaseRepository = supabaseRepository;
            _accountRepository = accountRepository;
            _doctorRepository = doctorRepository;
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

        [HttpGet("supabase/login/{authId}")]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(Task<BaseResponse<PatientAuthResponse<Patient>>>))]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int) HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> LoginUser( string authId)
        {
            Log.Information("Request:{request}", authId);
            var response1 = await _doctorRepository.GetAllDoctor();
            var response = await _doctorRepository.GetDoctor(authId);
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

        [HttpGet("sso/login/{authId}")]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(AppWriteSession))]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int) HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> LoginSSOUser( string authId)
        {
            //var response  = await _appWriteRepository.LoginUser(user);
            Log.Information($"Request:{authId}");

            var response = await _doctorRepository.GetDoctor(authId);

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