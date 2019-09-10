using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using SerapisMedicalAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PracticeController : Controller
    {
       
        private readonly IPracticeRepository _practiceRepository;
        public PracticeController(IPracticeRepository practiceRepository)
        {
            _practiceRepository = practiceRepository;
        }

        // GET: api/Practice
        [HttpGet]
        //[Route("api/Practice")]
        public async Task<IEnumerable<Practice>> Get()
        {
            // -/api/practice
            
            return await _practiceRepository.GetPractices();
        }

        // GET: api/Practice/5bc9bd861c9d4400001badf1
        [HttpGet("{id}", Name = "GetPractice")]
        public async Task<IActionResult> Get(string id)
        {
            ObjectId parm = ObjectId.Parse(id);
            var practice = await _practiceRepository.GetPracticeById(parm);

            if (practice == null)
                return new NotFoundResult();

            return new ObjectResult(practice);

        }

        // POST: api/Practice
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Practice/5
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
