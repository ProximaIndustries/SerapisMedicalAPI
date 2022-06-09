using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model.Symptoms;
using SerapisMedicalAPI.Services.SymptomsChecker;

namespace SerapisMedicalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SymptomCheckerController : Controller
    {
        private readonly ISymptomCheckerRepository _symptomCheckerRepository;
        private readonly ISymptomsCheckerService _symptomsCheckerService;
        private readonly ILogger<SymptomCheckerController> _logger;

        public SymptomCheckerController(ISymptomCheckerRepository symptomCheckerRepository, ILogger<SymptomCheckerController> logger,
            ISymptomsCheckerService symptomsCheckerService)
        {
            _symptomCheckerRepository = symptomCheckerRepository;
            _logger = logger;
            _symptomsCheckerService = new Apimedic();
        }
        
        // GET: api/SymptomChecker
        [HttpGet("v1/symptoms")]
        public async Task<IActionResult> Get()
        {
            var result = await _symptomCheckerRepository.GetAllSymptoms();

            return Ok(result);
        }
        // GET: api/SymptomChecker/v1/symptoms/5-21-13
        [HttpGet("v1/symptoms/{id}")]
        public Symptoms GetDiagnosisBySymptoms()
        {
            var result = _symptomCheckerRepository.GetSymptomById();

            return result;
        }
        
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<DiagnosisResponse>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(DiagnosisResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(DiagnosisResponse))]
        // GET: api/SymptomChecker/v1/diagnosis-by-symptoms?id= &age= &sex=
        [HttpGet("v1/diagnosis-by-symptoms")]
        public Task<IActionResult> GetDiagnosisBySymptomsIdAgeGender([FromQuery]string id, string age, string sex)
        {
            var result = _symptomCheckerRepository.GetProposedDiagnosisBySymptoms(id,age,sex);
            
            //IEnumerable<DiagnosisResponse> value = _symptomsCheckerService.GetProposedDiagnosisBySymptoms("male", "1984", arr);

            if (result == null)
            {
                return Task.FromResult<IActionResult>(BadRequest(result));
            }
            return Task.FromResult<IActionResult>(Ok(result));
        }
        
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<DiagnosisResponse>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(DiagnosisResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(DiagnosisResponse))]
        // GET: api/SymptomChecker/v1/diagnosis-by-symptoms?id= &age= &sex=
        [HttpGet("v1/proposed-symptoms")]
        public async Task<IActionResult> GetProposedSymptomsByIdAgeGender([FromQuery]string id, string age, string sex)
        {
            var result = await _symptomsCheckerService.GetProposedSymptoms(sex,age,id);
            
            //IEnumerable<DiagnosisResponse> value = _symptomsCheckerService.GetProposedDiagnosisBySymptoms("male", "1984", arr);

            if (result == null)
            {
                return (BadRequest(result));
            }
            return (Ok(result));
        }
    }
}
