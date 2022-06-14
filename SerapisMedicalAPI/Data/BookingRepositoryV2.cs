using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using SerapisMedicalAPI.Data.Base;
using SerapisMedicalAPI.Helpers;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.AppointmentModel;
using SerapisMedicalAPI.Model.Bookings;
using SerapisMedicalAPI.Model.DoctorModel.Practice;
using Serilog;

namespace SerapisMedicalAPI.Data
{
    public class BookingRepositoryV2 :  IBookingRepositoryV2
    {
        private readonly Context _context;
        private readonly IPatientRepository _patientRepository;
        private readonly IPracticeRepository _practiceRepository;
        private readonly IDoctorRepository _doctorRepository;
        public BookingRepositoryV2(Context context, IPatientRepository patientRepository,
            IPracticeRepository practiceRepository,
            IDoctorRepository doctorRepository)
        {
            _context = context;
            _patientRepository = patientRepository;
            _practiceRepository = practiceRepository;
            _doctorRepository = doctorRepository;
        }



        public async Task<BaseResponse<Booking>> MakeBooking(Booking appointment)
        {
            try
            {
                var bookingObj = new Booking(
                    appointment.id = ObjectId.GenerateNewId().ToString(),
                    appointment.BookingId,appointment.PracticeId,
                    appointment.DoctorsId,appointment.BookedAppointment,appointment.AppointmentDateTime,appointment.HasSeenGP );
                
                await _context
                    .BookingsCollection
                    .InsertOneAsync(bookingObj);
                
                //var result = await _context.BookingsCollection.FindAsync(x=> x)
                
                return new BaseResponse<Booking> {  isSuccesful = true, data = appointment, message = "Success", StatusCode = StatusCodes.Successful};
            }
            catch (Exception e)
            {
                Log.Error(e.ToJson());
                return new BaseResponse<Booking>()
                    {isSuccesful = false, data = null, message = "UnSuccessful", StatusCode = StatusCodes.UnSuccessful};
            }
            
        }

        public async Task<bool> AddBooking(ObjectId practiceid, AppointmentDto appointmentBooking)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<IEnumerable<BookingDTO>>> GetAllBookings()
        {
            var bookinglist = new List<BookingDTO>(); 
            try
            {
                var bookingResult = await _context.BookingsCollection
                    .Find(_ => true)
                    .ToListAsync();

                foreach (var booking in bookingResult)
                {
                    var patient = await _patientRepository.GetPatientById(booking.BookedAppointment.BookedpatientId);
                    var bookingDto = new BookingDTO
                    {
                        BookingId = booking.BookingId,
                        Patient = patient.data,
                        PracticeId = booking.PracticeId,
                        DoctorsId = booking.DoctorsId,
                        BookedAppointment = booking.BookedAppointment,
                        AppointmentDateTime = booking.AppointmentDateTime,
                        HasSeenGP = booking.HasSeenGP,
                        CreatedDate = booking.CreatedDate
                    };
                    bookinglist.Add(bookingDto);
                }
                
                return  new BaseResponse<IEnumerable<BookingDTO>>
                    {isSuccesful = true, data = bookinglist, message = "Successful", StatusCode = StatusCodes.Successful};
            }
            catch (Exception e)
            {
                Log.Information(e.ToJson());
                return new BaseResponse<IEnumerable<BookingDTO>>()
                    {isSuccesful = false, data = null, message = "UnSuccessful", StatusCode = StatusCodes.UnSuccessful};
            }
        }

        //This is supposed to be used Primarly by the Practice 
        public async Task<BaseResponse<IEnumerable<Booking>>> GetBookingsByDate(string id , DateTime startDate)
        {
            try
            {
                var result = await _context.BookingsCollection
                    .Find(x => x.AppointmentDateTime.Date == startDate.Date)
                    .ToListAsync();

                //_patientRepository.GetPatientById(id);

                return new BaseResponse<IEnumerable<Booking>>
                    { isSuccesful = true, data = result, message = "Successful", StatusCode = StatusCodes.Successful };
            }
            catch (Exception e)
            {
                Log.Information(e.ToJson());
                return new BaseResponse<IEnumerable<Booking>>()
                    { isSuccesful = false, data = null, message = "UnSuccessful", StatusCode = StatusCodes.UnSuccessful };
            }
        }

        public async Task<BaseResponse<IEnumerable<PatientBookingDto>>> GetBookingsByPatientId(string id)
        {
            var bookinglist = new List<PatientBookingDto>(); 
            try
            {
                var results = (await _context.BookingsCollection
                    .FindAsync(x => x.BookedAppointment.BookedpatientId == id) ).ToList();
                foreach (var booking in results)
                {
                    var doctor = await _doctorRepository.GetDoctor(booking.DoctorsId);
                    var practice = await _practiceRepository.GetPracticeById(booking.PracticeId);
                    
                    var bookingDto = new PatientBookingDto
                    {
                        BookingId = booking.BookingId,
                        DoctorName = $"DR {doctor?.FirstName.Substring(0,1)} {doctor?.LastName}",
                        PracticeName = $"{practice?.PracticeName}",
                        AppointmentDateTime = booking.AppointmentDateTime,
                    };
                    bookinglist.Add(bookingDto);
                }
                return new BaseResponse<IEnumerable<PatientBookingDto>>
                    { isSuccesful = true, data = bookinglist, message = "Successful", StatusCode = StatusCodes.Successful };
            }
            catch (Exception e)
            {
                Log.Information(e.ToJson());
                return new BaseResponse<IEnumerable<PatientBookingDto>>()
                    { isSuccesful = false, data = null, message = "UnSuccessful", StatusCode = StatusCodes.UnSuccessful };
            }
        }
        public async Task<BaseResponse<IEnumerable<BookingDTO>>> GetBookingsByPracticeId(string id)
        {
            var bookinglist = new List<BookingDTO>(); 
            try
            {
                var result = (await _context.BookingsCollection
                    .FindAsync(x => x.PracticeId == id)).ToEnumerable();
                
                foreach (var booking in result)
                {
                    var patient = await _patientRepository.GetPatientById(booking.BookedAppointment.BookedpatientId);
                    var bookingDto = new BookingDTO
                    {
                        BookingId = booking.BookingId,
                        Patient = patient.data,
                        PracticeId = booking.PracticeId,
                        DoctorsId = booking.DoctorsId,
                        BookedAppointment = booking.BookedAppointment,
                        AppointmentDateTime = booking.AppointmentDateTime,
                        HasSeenGP = booking.HasSeenGP,
                        CreatedDate = booking.CreatedDate
                    };
                    bookinglist.Add(bookingDto);
                }
                return new BaseResponse<IEnumerable<BookingDTO>>
                    { isSuccesful = true, data = bookinglist, message = "Successful", StatusCode = StatusCodes.Successful };
            }
            catch (Exception e)
            {
                Log.Information(e.ToJson());
                return new BaseResponse<IEnumerable<BookingDTO>>()
                    { isSuccesful = false, data = null, message = "UnSuccessful", StatusCode = StatusCodes.UnSuccessful };
            }
        }

        public async Task CancelBooking(object _id)
        {
            throw new NotImplementedException();
        }

        public async Task PostponeBooking(object _id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppointmentDao>> GetBookedPatientsAsync(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}