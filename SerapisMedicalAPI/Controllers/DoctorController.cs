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

        // GET: api/Doctor/5
        
        [HttpGet]
        [QueryStringContraint("id",true)]

        public async Task<IActionResult> AutenticateUser(string id, string password)
        {
            

            Doctor doc = await _doctorRepository.AuthenticateDoctor(id, password);
            
            if(doc == null)
            {

                return BadRequest(doc);
            }
            return new OkObjectResult(doc)
            //response.DidError = true;
            //response.ErrorMessage = "There was an internal error, please contact to technical support."
            // Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetStockItemAsync), ex);

            ;
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
