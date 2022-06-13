using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.AppointmentModel;
using SerapisMedicalAPI.Model.Bookings;
using SerapisMedicalAPI.Model.DoctorModel.Practice;

namespace SerapisMedicalAPI.Interfaces
{
    public interface IBookingRepositoryV2
    {
        //Create-- Patient uses this method
        Task<BaseResponse<Booking>> MakeBooking(Booking appointment);
        //Create-- Make a booking for the patient
        Task<bool> AddBooking(ObjectId practiceid, AppointmentDto appointmentBooking);

        //Read-- Get available bookings
        Task<BaseResponse<IEnumerable<BookingDTO>>> GetAllBookings();

        Task<BaseResponse<IEnumerable<BookingDTO>>> GetBookingsByPracticeId(string id);
        Task<BaseResponse<IEnumerable<PatientBookingDto>>> GetBookingsByPatientId(string id);
        
        //Delete-- Remove booking
        //This comes with fees, will add logic later on so leave for now
        Task CancelBooking(object _id);

        //Update-- Postpone booking, (leave for now, for version 2.5 or something)
        Task PostponeBooking(object _id);

        //Create-- Get bookings for today
        Task<IEnumerable<AppointmentDao>> GetBookedPatientsAsync(DateTime date);
    }
}