using Microsoft.AspNetCore.Mvc;
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
    public class MarketingController : ControllerBase
    {
        private readonly IMarketing _iMarketing;
        public MarketingController(IMarketing iMarketing)
        {
            _iMarketing = iMarketing;
        }

        // GET: api/<MarketingController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MarketingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MarketingController>
        //Adds a new client to the database from website enquiries
        [HttpPost("addcontact")]
        public void PostContact([FromBody] Contact contact)
        {
            _iMarketing.AddContactToSendGridDatabase(contact);
        }

        // PUT api/<MarketingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MarketingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
