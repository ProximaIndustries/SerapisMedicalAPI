﻿using Microsoft.AspNetCore.Mvc;
using SerapisMedicalAPI.Helpers;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Services;
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
        private readonly IBilling _iBilling;

        public BillingController(IBilling iBilling)
        {
            _iBilling = iBilling;
        }

        // GET: api/<BillingController>
        [HttpGet]
        public object Get()
        {
            return _iBilling.StartSubPlan().Result;
        }

        // GET api/<BillingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BillingController>
        [HttpPost]
        public object PostNewClientOnBoard([FromBody] string value)
        {
            //_iBilling.OnBoardNewClient();
            _iBilling.StartSubPlan();

            return value;
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
