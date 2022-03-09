using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model.Symptoms;
using SerapisMedicalAPI.Services.SymptomsChecker;

namespace SerapisMedicalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ISymptomsCheckerService _symptomsCheckerService;
        private readonly ISymptomCheckerRepository _symptomCheckerRepository;
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(
            ISymptomsCheckerService symptomsCheckerService,
            ILogger<ValuesController> logger,
            ISymptomCheckerRepository symptomCheckerRepository
            )
        {
            _symptomsCheckerService = symptomsCheckerService;
            _logger = logger;
            _symptomCheckerRepository = symptomCheckerRepository;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<DiagnosisResponse> Get()
        {
            
            int[] arr = new[] {234, 11, 16};
            IEnumerable<DiagnosisResponse> value = _symptomsCheckerService.GetProposedDiagnosisBySymptoms("male", "1984", arr);
            //var valueEnum = value.ToList();
            //_logger?.LogInformation("The number of Symptoms being returned is: " + value.ToList().Count);
            
            //IEnumerable<Symptoms> symptoms = (IEnumerable<Symptoms>)_symptomsCheckerService.GetAllSymptoms();
           
           // var symptomsEnumerable = symptoms.ToList();
            //_logger?.LogInformation("The number of Symptoms being returned is: " + symptomsEnumerable.ToList().Count);
            //_logger?.LogInformation("Populating Symptoms into Cassandra ");
            
            //_symptomCheckerRepository.PopulateSymptoms(symptomsEnumerable);
            
            //return symptomsEnumerable;
            //return valueEnum;
            return null;
        }

        // GET api/values/24
        [HttpGet("{age}")]
        public ActionResult<string> Get(int id)
        {
            
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
