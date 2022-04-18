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
            var message = new ClickATellMessage
            {
                channel = "sms",
                to = "27817004798",
                //Content = "Serapis Medical: `\n` Your OTP is 8211"
                content = "Serapis Medical: Your OTP is 8211"
            };
            
            
            await _messagingRepository.SendOTPSms(messaging);

            return StatusCode(200);
        }


    }
}
