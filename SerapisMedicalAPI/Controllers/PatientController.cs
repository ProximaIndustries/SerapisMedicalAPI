using Microsoft.AspNetCore.Mvc;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

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
        public async Task<IEnumerable<PatientUser>> Get()
        {
            return await _patientRepository.GetAllPatients();
        }

        // GET: api/Patient/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<PatientUser> GetPatientMedicalInformation(ObjectId _id)
        {

            if (_id != null)
            {
                try
                {
                    //Try get the patients information
                    //var predicateFilter=filter
                    //var patientsFile=_context.PatientCollection.FindAsync(predicateFilter);
                    return null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                //Return some error message
                return null;
            }
        }

        //RegisterController
        //      [Route("api/Patient/Register")]
        //public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        //      {
        //          if(!ModelState.IsValid)
        //          {
        //              return BadRequest(ModelState);
        //          }
        //          var user = new PatientUser() {
        //              //add attributes here
        //          };
        //          IdentityResult result = await userManager.CreateAsync(user,model.Password);

        //          if(!result.Succeeded)
        //          {
        //              return GetErrorResult(result);
        //          }
        //          return Ok();
        //      }


        // POST: api/Patient
        [HttpPost]
        public void Post([FromBody]string value)
        {
           
        }

        // PUT: api/Patient/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
