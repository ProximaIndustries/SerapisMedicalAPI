using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerapisMedicalAPI.Model.Symptoms;
using SerapisMedicalAPI.Services.SymptomsChecker;

namespace SerapisMedicalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ISymptomsCheckerService _symptomsCheckerService;
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ISymptomsCheckerService symptomsCheckerService, ILogger<ValuesController> logger)
        {
            _symptomsCheckerService = symptomsCheckerService;
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Symptoms> Get()
        {
            
            IEnumerable<Symptoms> symptoms = (IEnumerable<Symptoms>)_symptomsCheckerService.GetAllSymptoms();
            _logger?.LogInformation("The number of Symptoms being returned is: " + symptoms.ToList().Count);
            return symptoms;
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
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
