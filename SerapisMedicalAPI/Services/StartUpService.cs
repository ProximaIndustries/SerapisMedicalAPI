using Microsoft.Extensions.Logging;
using SerapisMedicalAPI.Services.SymptomsChecker;

namespace SerapisMedicalAPI.Services
{
    public class StartUpService
    {
        private readonly ISymptomsCheckerService _symptomsCheckerService;
        private readonly ILogger<StartUpService> _logger;

        public StartUpService(ISymptomsCheckerService symptomsCheckerService, ILogger<StartUpService> logger)
        {
            _symptomsCheckerService = symptomsCheckerService;
            _logger = logger;
        }

        public void m()
        {
            
        }
    }
}