using System;
using SerapisMedicalAPI.Model.AppointmentModel;

namespace SerapisMedicalAPI.Model.Bookings
{
    public class PatientBookingDto 
    {
        
        public string id  { get; set; }
        public string BookingId { get;  set; }
        public string DoctorName { get; set; }
        public string PracticeName { get; set; }
        public DateTime AppointmentDateTime { get; set; }
    }
}