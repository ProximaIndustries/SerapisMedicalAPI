using Microsoft.AspNetCore.Mvc;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using SerapisMedicalAPI.Model.PatientModel;

namespace SerapisMedicalAPI
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PatientController : Controller
    {

        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        // GET: api/patient
        [HttpGet]
        public async Task<IEnumerable<Patient>> Get()
        {
            return await _patientRepository.GetAllPatients();
        }

        // GET: api/Patient/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientMedicalInformation(string id)
        {

            if (id != null)
            {
                //ObjectId parm = ObjectId.Parse(id);
                var _patient = await _patientRepository.GetPatientById(id);

                if (_patient == null)
                    return new NotFoundResult();

                return new ObjectResult(_patient);
            }
            else
            {
                //Return some error message
                return new NotFoundResult();
            }
        }



        // POST: api/Patient
        [HttpPost]
        public void Post([FromBody]string value)
        {
           
        }

        // PUT: api/Patient/5
        [HttpPut]
        public void Put( [FromBody] Patient value)
        {
            _patientRepository.EditPatientUser(value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
