using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using SerapisMedicalAPI.Data;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using System.Text.Encodings.Web;
using SerapisMedicalAPI.Helpers;

namespace SerapisMedicalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DoctorController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        
        public DoctorController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        // GET: api/Doctor
        [HttpGet(Name = "GetAllDoctors")]
        
        public async Task<IEnumerable<Doctor>> Get()
        {
            return await _doctorRepository.GetAllDoctor();
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
            if (id != null)
            {

            }

        }
    }
}
