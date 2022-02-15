using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Interfaces;

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
        [HttpGet(Name = "Get")]
        public void Get()
        {
            
        }
        
        
    }
}
