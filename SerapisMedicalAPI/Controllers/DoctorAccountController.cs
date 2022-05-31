using Microsoft.AspNetCore.Mvc;
using SerapisMedicalAPI.Helpers;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SerapisMedicalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorAccountController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorAccountController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        // GET: api/Doctor?id
        [HttpGet(Name = "Login Doctor")]
        [QueryStringContraint("id", true)]

        public async Task<IActionResult> AutenticateUser(string id, string password)
        {
            // Verify Credentials and retrieve user

            Doctor doc = await _doctorRepository.AuthenticateDoctor(id, password);
            if (doc == null)
                return BadRequest(doc);

            // Send OTP

            // Verify OTP

            // Send Doctor all his User Payload only

            return new OkObjectResult(doc);
            //response.DidError = true;
            //response.ErrorMessage = "There was an internal error, please contact to technical support."
            // Logger?.LogCritical("There was an error on '{0po' invocation: {1}", nameof(GetStockItemAsync), ex);


        }

        // GET api/<DoctorAccountController1>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DoctorAccountController1>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<DoctorAccountController1>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DoctorAccountController1>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
