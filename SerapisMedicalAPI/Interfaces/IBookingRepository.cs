using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.AppointmentModel;
using SerapisMedicalAPI.Model.DoctorModel.Practice;
using SerapisMedicalAPI.Model.PatientModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Interfaces
{
    public interface IBookingRepository
    {
        //Create-- Patient uses this method
        Task<bool> MakeBooking(PracticeInformation practice);
        //Create-- Make a booking for the patient
        Task<bool> AddBooking(PracticeInformation practice, AppointmentDto appointmentBooking);

        //Read-- Get availiable bookings
        Task<IEnumerable<Appointment>> GetAllAvaliableBookings(IPracticeRepository maxPracticeDistance);

        //Delete-- Remove booking
        //This comes with fees, will add logic later on so leave for now
        Task CancelBooking(object _id);

        //Update-- Postpone booking, (leave for now, for version 2.5 or something)
        Task PostponeBooking(object _id);

        //Create-- Get bookings for today
        Task<IEnumerable<Appointment>> GetBookedPatientsAsync(DateTime date);
    }
}
