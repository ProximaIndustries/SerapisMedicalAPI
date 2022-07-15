using Microsoft.AspNetCore.Mvc;
using SerapisMedicalAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SerapisMedicalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        // GET: api/<BillingController>
        [HttpGet]
        public string Get()
        {
            return SignatureGenerator.GenerateApiSignature();
        }

        // GET api/<BillingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BillingController>
        [HttpPost("newclient")]
        public void Post([FromBody] string value)
        {
            //All the methods must be 
        }

        // PUT api/<BillingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BillingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
