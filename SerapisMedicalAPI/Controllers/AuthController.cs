using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using SerapisMedicalAPI.Helpers;
using SerapisMedicalAPI.Model;
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
        private readonly IAccountSupabaseRepository _supabaseRepository;
        public AuthController(IAccountSupabaseRepository supabaseRepository)
        {
            _supabaseRepository = supabaseRepository;
        }
        [HttpPost("supabase/register")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse<Patient>))] 
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult>  RegisterUser([FromBody] Patient patient)
        {
            Log.Information($"Request:{patient?.ToJson()}");
            var response = await _supabaseRepository.RegisterUser(patient);
            
            return new OkObjectResult(response);
        }
        
        [HttpPost("supabase/login")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Task<BaseResponse<PatientAuthResponse>>))] 
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult>  LoginUser([FromBody] SupabaseAuth patient)
        {
            Log.Information($"Request:{patient?.ToJson()}");
            var response = await _supabaseRepository.LoginUser(patient);
            var encrypt = EncryptService.EncryptPlainTextToCipherText(response.ToJson().ToString());
            var decrypt = EncryptService.DecryptCipherTextToPlainText(encrypt);
            Log.Information(encrypt);
            Log.Information(decrypt);
            Log.Information($"Request:{response?.ToJson()}");
            return new OkObjectResult(response);
        }
        
        [HttpPost("sso/register")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Task<BaseResponse<PatientAuthResponse>>))] 
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RegisterSSOUser([FromBody] SupabaseAuth patient)
        {
            Log.Information($"Request:{patient?.ToJson()}");
           
            var response = await _supabaseRepository.LoginUser(patient);
           
            Log.Information($"Request:{response?.ToJson()}");
            return new OkObjectResult(response);
        }
        
        [HttpPost("sso/login")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Task<BaseResponse<PatientAuthResponse>>))] 
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> LoginSSOUser([FromBody] SupabaseAuth patient)
        {
            Log.Information($"Request:{patient?.ToJson()}");
           
            var response = await _supabaseRepository.LoginUser(patient);
           
            Log.Information($"Request:{response?.ToJson()}");
            return new OkObjectResult(response);
        }
        
        [HttpPut("sso/linkUserWithPractice")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Task<BaseResponse<PatientAuthResponse>>))] 
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> LinkSSOUserWithPractice([FromBody] SupabaseAuth patient)
        {
            Log.Information($"Request:{patient?.ToJson()}");
           
            var response = await _supabaseRepository.LoginUser(patient);
           
            Log.Information($"Request:{response?.ToJson()}");
            return new OkObjectResult(response);
        }
        
        [HttpPut("sso/deLinkUserWithPractice")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Task<BaseResponse<PatientAuthResponse>>))] 
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> DeLinkSSOUserWithPractice([FromBody] SupabaseAuth patient)
        {
            Log.Information($"Request:{patient?.ToJson()}");
           
            var response = await _supabaseRepository.LoginUser(patient);
           
            Log.Information($"Request:{response?.ToJson()}");
            return new OkObjectResult(response);
        }
    }
}
