using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Services;

namespace SerapisMedicalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommuncationsController : ControllerBase
    {
        // GET: api/Communcations
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Communcations/5
        [HttpGet("{number}", Name = "Getsms")]
        public async Task<bool> GetAsync(string number)
        {
            return await TextingService.SendMessageAsync(number, "sms", "Greetings from Serapis Medical APi");
        }

        // POST: api/Communcations
        [HttpPost]
        public async Task<bool> Post([FromBody] Clickatell message)
        {
            return await TextingService.SendMessageAsync(message.messages.FirstOrDefault().to, "sms", "Greetings from Serapis Medical APi");
        }

        // PUT: api/Communcations/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
