using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SerapisMedicalAPI.Model.PatientModel;

namespace SerapisMedicalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Payment")]
    public class PaymentController : Controller
    {
        // GET: api/Payment
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Payment/5
        [HttpGet("{id}", Name = "GetPayment")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Payment
        [HttpPost]
        public IActionResult Post([FromBody]PaymentModel payment)
        {
            // only receive the token here, no credit card information is coming to the server at all. 
            //You will want to make sure your API is using HTTPS and you provide any further authentication as necessary.

            // You can optionally create a customer first, and attached this to the CustomerId
            // var charge = new StripeChargeCreateOptions
            //{
            //    Amount = Convert.ToInt32(payment.Amount * 100), // In cents, not rands, times by 100 to convert
            //    Currency = "zar", // or the currency you are dealing with
            //    Description = "something awesome",
            //    SourceTokenOrExistingSourceId = payment.Token
            //};


            //var service = new StripeChargeService("sk_test_xxxxxxxxxxxxx");


            // Ideally you would put in additional information, but you can just return true or false for the moment.
            return Ok(true);
        }

        // PUT: api/Payment/5
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
