﻿using Microsoft.AspNetCore.Mvc;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using SerapisMedicalAPI.Model.PatientModel;
using System.Diagnostics;

namespace SerapisMedicalAPI
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PatientController : Controller
    {

        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        // GET: api/patient
        [HttpGet]
        public async Task<IEnumerable<Patient>> Get()
        {
            return await _patientRepository.GetAllPatients();
        }

        // GET: api/Patient/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientMedicalInformation(string id)
        {

            if (id != null)
            {
                //ObjectId parm = ObjectId.Parse(id);
                var _patient = await _patientRepository.GetPatientById(id);

                if (_patient == null)
                    return new NotFoundResult();

                return new ObjectResult(_patient);
            }
            //Return some error message
            return new NotFoundResult();
        }


        // PUT: api/Patient/
        [HttpPut]
        public async Task<BaseResponse<Patient>> Put([FromBody] Patient patient)
        {
            var response = await _patientRepository.EditPatientUser(patient);

            return response;

        }
    }
}
