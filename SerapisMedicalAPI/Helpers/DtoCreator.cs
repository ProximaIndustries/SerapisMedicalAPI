using MongoDB.Bson;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.AppointmentModel;
using SerapisMedicalAPI.Model.DoctorModel.Doctor;
using SerapisMedicalAPI.Model.DoctorModel.Practice;
using SerapisMedicalAPI.Model.PatientModel;
using SerapisMedicalAPI.Model.PracticeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Helpers
{
    public static class DtoCreator
    {
        public static PracticeDto CreatePracticeDto(PracticeInformation practice) 
        {
            PracticeDto newDto = new PracticeDto()
            {
                PracticeID = practice.Id,
                ContactNumber = practice.ContactPractice.PracticeTelephoneNumber,
                PracticeName = practice.PracticeName,
                NumberOfPatientsAtPractice = practice.NumOfPatientsInPractice,
                OperatingTimes = practice.OperatingTime,
                PracticePicture = practice.PracticePicture
            };

            return newDto;
        }

        public static DoctorDto CreateDoctorDto(Doctor doctor)
        {
            DoctorDto newDocDto = new DoctorDto()
            {
                DoctorId = doctor.User.UserId,
                FullName = string.Format(doctor.FirstName + " " + doctor.LastName),
                ProfilePicture=doctor.ProfilePicture,
                TimeAvailable=doctor.ListOfAppointments.FirstOrDefault().TimeBooked,
                University=doctor.Qualifications.FirstOrDefault().University
            };

            return newDocDto;
        }

        public static PatientDto CreatePatientDto(Patient patient)
        {
            PatientDto newPatientDto = new PatientDto()
            {
                  FullName=string.Format(patient.PatientFirstName+" "+ patient.PatientLastName),
                  MedicalAidPatient=patient.MedicalAidPatient,
                  PatientId=ObjectId.Parse(patient.SocialID),
                  //AppointmentSet=GetAppointment(objectId id);
                  ProfilePicture=patient.PatientProfilePicture
            };

            return newPatientDto;
        }

        public static AppointmentDto CreateAppointmentDto(Appointment appointment)
        {
            AppointmentDto newAppointmentDto = new AppointmentDto()
            {
                 AppointmentId=appointment.BookingId,
                 DateBooked=DateTime.Parse(appointment.DateBooked),
                 TimeBooked=DateTime.Parse(appointment.TimeBooked)
            };

            return newAppointmentDto;
        }
    }
}
