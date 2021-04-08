using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using SerapisMedicalAPI.Data;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.AppointmentModel;
using SerapisMedicalAPI.Model.DoctorModel.Practice;
using SerapisMedicalAPI.Model.PatientModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPracticeRepository _practiceRepository;
        private readonly IPatientRepository _patientRepository;
        public BookingController(IPatientRepository patientRepository, IPracticeRepository practiceRepository, IBookingRepository bookingRepository)
        {
            _patientRepository = patientRepository;
            _practiceRepository = practiceRepository;
            _bookingRepository = bookingRepository;
        }

        //eg. http://localhost:53617/api/Booking/date/2018-01-01
        // GET: api/Booking/date
        [HttpGet(template: "{date}")]
        public async Task<IEnumerable<AppointmentDao>> GetPatientsBookedForTheDay(DateTime date)
        {
            //rather past through a string then convert it to a DateTime object here. 
            var appointmentForToday = await _bookingRepository.GetBookedPatientsAsync(date);

            return appointmentForToday;
        }


        // POST: api/Booking/id
        [HttpPost]
        public async void Post([FromBody] string value)
        {
            PracticeInformation practice = new PracticeInformation();

            AppointmentDto appointment = new AppointmentDto();

            //var appointmentMade = await _bookingRepository.AddBooking(practice, appointment);
        }


        //Use this one for booking Please (30/03/2021)
        //[HttpPatch("{id}")]
        [HttpPut]
        public async Task<IActionResult> Patch([FromQuery]string id,[FromBody] AppointmentDto booking) 
        {
            //PracticeInformation practice = new PracticeInformation();

            
            ObjectId p1 = ObjectId.Parse(id);


            /*PracticeInformation practice = await _practiceRepository.GetPracticeById(p1.Id);

            if (practice == null)
                return new NotFoundResult();*/

            bool response = await _bookingRepository.AddBooking(p1, booking);
            Debug.WriteLine(" Booking Creation Response =>[" + response + "]");
            if (response == true)
            {
                return StatusCode(201);
            }
            return StatusCode(400);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
