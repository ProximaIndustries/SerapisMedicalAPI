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
        
        [HttpGet]
        public IEnumerable<string> defaultroute()
        {
            Log.Information("Request:");
            return new string[] { "value1", "value2" };
        }
    }
}
