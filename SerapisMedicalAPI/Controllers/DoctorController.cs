using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using Microsoft.Extensions.Logging;

namespace SerapisMedicalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DoctorController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(IDoctorRepository doctorRepository, ILogger<DoctorController> logger)
        {
            _doctorRepository = doctorRepository;
            _logger = logger;
        }

        // GET: api/Doctor
        [HttpGet(Name = "GetAllDoctors")]
        
        public async Task<IEnumerable<Doctor>> Get()
        {
            IEnumerable<Doctor> allDoctors = await _doctorRepository.GetAllDoctor();

            _logger?.LogInformation("The number of doctors being returned is: "+ allDoctors.ToList().Count);
            _logger?.LogInformation("The number of doctors being returned is: {@allDoctors} ",allDoctors);

            return allDoctors;
        }

        [HttpGet("action1")]

        public IActionResult Get2()
        {
            return RedirectToAction("Get");
            //return await _doctorRepository.GetAllDoctor();
        }



        // POST: api/Doctor
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Doctor doctor)
        {
            await _doctorRepository.AddDoctor(doctor);
            return new OkObjectResult(doctor);
        }

        // PUT: api/Doctor/{objectId}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]Doctor doctor)
        {
            var doctorFromDb = await _doctorRepository.GetDoctor(id);

            if (doctorFromDb == null)
                return new NotFoundResult();

            doctor.User.HealthId = doctorFromDb.User.HealthId;

            await _doctorRepository.EditDoctor(doctor);

            return new OkObjectResult(doctor);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete([FromBody]int id)
        {


        }
    }
}
