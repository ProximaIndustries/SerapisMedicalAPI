using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SerapisMedicalAPI.Interfaces;

namespace SerapisMedicalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfirmationMailingController : ControllerBase
    {
        private readonly IMailing mail;

        public ConfirmationMailingController(IMailing _mail)
        {
            mail = _mail;
        }

        // GET: api/ConfirmationMailing
        [HttpGet]
        public IEnumerable<string> GetAllEmails()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ConfirmationMailing/5
        [HttpGet("{id}", Name = "GetConfirmation")]
        public void GetEmailConfirmation(int id)
        {

        }

        // POST: api/ConfirmationMailing
        [HttpPost]
        public void PostEmail([FromBody] string email)
        {
            try
            {
                //Send the confirmation
                mail.SendConfirmation(email);
            }
            catch (Exception ex)
            {
                //log the error
                throw ex;
            }
        }

        // PUT: api/ConfirmationMailing/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
