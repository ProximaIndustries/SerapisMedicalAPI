using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model.Symptoms;

namespace SerapisMedicalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DiagnosisController : Controller
    {
        private readonly ISymptomCheckerRepository _symptomCheckerRepository;
        private readonly ILogger<DiagnosisController> _logger;
    }
}