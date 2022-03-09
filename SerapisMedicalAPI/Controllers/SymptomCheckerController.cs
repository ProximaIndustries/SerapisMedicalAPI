using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model.Symptoms;

namespace SerapisMedicalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SymptomCheckerController : Controller
    {
        private readonly ISymptomCheckerRepository _symptomCheckerRepository;
        private readonly ILogger<SymptomCheckerController> _logger;

        public SymptomCheckerController(ISymptomCheckerRepository symptomCheckerRepository, ILogger<SymptomCheckerController> logger)
        {
            _symptomCheckerRepository = symptomCheckerRepository;
            _logger = logger;
        }
        
        // GET: api/SymptomChecker
        [HttpGet("v1/symptoms")]
        public Symptoms Get()
        {
            var result = _symptomCheckerRepository.GetSymptomById();

            return result;
        }
        // GET: api/SymptomChecker/5
        [HttpGet("v1/symptoms/{billerId}")]
        public Symptoms GetDiagnosisBySymptoms()
        {
            var result = _symptomCheckerRepository.GetSymptomById();

            return result;
        }
        
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<DiagnosisResponse>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(DiagnosisResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(DiagnosisResponse))]
        // GET: api/SymptomChecker/5-2-1
        [HttpGet("v1/diagnosis-by-symptoms/{id}")]
        public Task<IActionResult> GetDiagnosisBySymptoms(string id)
        {
            var result = _symptomCheckerRepository.GetProposedDiagnosisBySymptoms(id);
            
            //IEnumerable<DiagnosisResponse> value = _symptomsCheckerService.GetProposedDiagnosisBySymptoms("male", "1984", arr);

            return Task.FromResult<IActionResult>(Ok(result));
        }
    }
}
