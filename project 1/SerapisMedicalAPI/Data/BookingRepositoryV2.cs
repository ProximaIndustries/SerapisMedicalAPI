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
        private readonly Context _context ;
        public BookingRepositoryV2(Context context )
        {
            _context = context;
        }
    


        public async Task<BaseResponse<Booking>> MakeBooking(Booking appointment)
        {
            try
            {
                var bookingObj = new Booking(
                    appointment.id = appointment.id,
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

        public async Task<BaseResponse<IEnumerable<Booking>>> GetAllBookings()
        {
            try
            {
                var result = await _context.BookingsCollection
                    .Find(_ => true)
                    .ToListAsync();

                return new BaseResponse<IEnumerable<Booking>>
                    {isSuccesful = true, data = result, message = "Successful", StatusCode = StatusCodes.Successful};
            }
            catch (Exception e)
            {
                Log.Information(e.ToJson());
                return new BaseResponse<IEnumerable<Booking>>()
                    {isSuccesful = false, data = null, message = "UnSuccessful", StatusCode = StatusCodes.UnSuccessful};
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