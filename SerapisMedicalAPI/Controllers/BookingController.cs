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
using SerapisMedicalAPI.Model.Bookings;
using Serilog;

namespace SerapisMedicalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPracticeRepository _practiceRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IBookingRepositoryV2 _bookingRepositoryV2;
        public BookingController(IPatientRepository patientRepository, IPracticeRepository practiceRepository,
            IBookingRepository bookingRepository, IBookingRepositoryV2 bookingRepositoryV2)
        {
            _patientRepository = patientRepository;
            _practiceRepository = practiceRepository;
            _bookingRepository = bookingRepository;
            _bookingRepositoryV2 = bookingRepositoryV2;
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

        [HttpGet]
        public async Task<BaseResponse<IEnumerable<BookingDTO>>> GetBookings()
        {
            //rather past through a string then convert it to a DateTime object here. 
            var response = await _bookingRepositoryV2.GetAllBookings();
            Log.Warning($"Response: {response.ToJson()}");
            return response;
        }
        
        // POST: api/Booking/
        [HttpPost]
        public async Task<BaseResponse<Booking>> Post([FromBody] Booking appointment)
        {
            Log.Warning($"Request: {appointment.ToJson()}");
            
            var response = await _bookingRepositoryV2.MakeBooking(appointment);
            Log.Warning($"Response: {response.ToJson()}");
            return response;
        }
        
        // POST: api/Booking/
        [HttpGet("{id}/practice")]
        public async Task<BaseResponse<IEnumerable<BookingDTO>>> GetByPracticeId(string id)
        {
            Log.Warning($"Request: {id.ToJson()}");
            
            var response = await _bookingRepositoryV2.GetBookingsByPracticeId(id);
            Log.Warning($"Response: {response.ToJson()}");
            return response;
        }

        //Use this one for booking Please (30/03/2021)
        //[HttpPatch("{id}")]
        [HttpPut]
        public async Task<IActionResult> Patch([FromQuery]string id, [FromBody] AppointmentDto booking) 
        {
            //PracticeInformation practice = new PracticeInformation();

            
            ObjectId p1 = ObjectId.Parse(id);
            
            if (booking == null)
                return new NotFoundResult();

            bool response = await _bookingRepository.AddBooking("p1", booking);
            Log.Information(" Booking Creation Response =>[" + response + "]"); 
            if (response == true)
            {
                return StatusCode(201);
            }
            return StatusCode(400);
        }

    }
}
