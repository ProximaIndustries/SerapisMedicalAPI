using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Controllers
{
    public class PrescriptionController : Controller
    {
        // GET: api/Prescription
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Prescription/5
        [HttpGet("{id}", Name = "GetPrescription")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Prescription
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Prescription/5
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
