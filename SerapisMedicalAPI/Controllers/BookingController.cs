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
        public async Task<IEnumerable<Appointment>> GetPatientsBookedForTheDay(DateTime date)
        {
            //rather past through a string then convert it to a DateTime object here. 
            var appointmentForToday = await _bookingRepository.GetBookedPatientsAsync(date);

            return appointmentForToday;
        }

        // GET: api/Booking/_id
        [HttpGet("{id}", Name = "GetBookedPatientFile")]
        [Route("api/[controller]/_id")]
        public Task<PatientUser> GetBookedPatientFile(ObjectId id)
        {
            if (id != null)
            {
                try
                {
                    var file = _patientRepository.GetPatientDetails(id);

                    return file;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return null;
            }
        }

        // POST: api/Booking/id
        [HttpPost]
        public async void Post([FromBody] string value)
        {
            PracticeInformation practice = new PracticeInformation();

            Appointment appointment = new Appointment();

            var appointmentMade = await _bookingRepository.AddBooking(practice, appointment);
        }

        // PUT: api/Booking/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]PracticeInformation practice)
        {
            

            PracticeInformation p1 = new PracticeInformation();
            p1.Id = ObjectId.Parse(id);
                
           
            practice = await _practiceRepository.GetPracticeById(p1.Id);

            if (practice == null)
                return new NotFoundResult();

            //practice.Appointments.Add(practice.Appointments);
            await _bookingRepository.MakeBooking(practice);

            return new OkObjectResult(practice);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(string id, [FromBody]Appointment booking)
        {
            PracticeInformation practice = new PracticeInformation();

            PracticeInformation p1 = new PracticeInformation();
            p1.Id = ObjectId.Parse(id);


            practice = await _practiceRepository.GetPracticeById(p1.Id);

            if (practice == null)
                return new NotFoundResult();

            //practice.Appointments.Add(practice.Appointments);
            await _bookingRepository.AddBooking(practice, booking);

            return new OkObjectResult(practice);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
