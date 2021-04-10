using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;

namespace SerapisMedicalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MessagingController : Controller
    {
        private readonly IMessagingRepository _messagingRepository;

        public MessagingController(IMessagingRepository messagingRepository)
        {
            _messagingRepository = messagingRepository;
        }

        // POST: /Messaging
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Messaging messaging)
        {

            await _messagingRepository.SendSms(messaging);

            return StatusCode(200);
        }


    }
}
