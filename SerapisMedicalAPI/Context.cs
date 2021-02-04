using MongoDB.Driver;
using MongoDB.Bson;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.PatientModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SerapisMedicalAPI.Model.PracticeModel;
using SerapisMedicalAPI.Model.AppointmentModel;
using SerapisMedicalAPI.Model.DoctorModel.Practice;
using System.Diagnostics;

namespace SerapisMedicalAPI
{
    public class Context
    {
        private readonly IMongoDatabase _database;

        public string OtherConnectionString = "mongodb+srv://KhanyiTheGreat:Langelihle1!@cluster0-i3gjx.azure.mongodb.net/SerapisMedical?retryWrites=true";
        public string ConnectionString = "mongodb+srv://Bonga:Langelihle1!@cluster0.bkjo1.mongodb.net/SerapisMedical?retryWrites=true&w=majority";
		
        


        public Context()
        {
            var client = new MongoClient(ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase("SerapisMedical");
            }
            else
            {
                Exception ex = new Exception();
                Debug.WriteLine(ex.ToString());
                throw ex;
               
            }

            
        }

        public IMongoCollection<Doctor> DoctorCollection => _database.GetCollection<Doctor>("MedicalProfessionals");
        //lambda way to do it
        public IMongoCollection<Appointment> BookingsCollection => _database.GetCollection<Appointment>("PatientBookings");

        public IMongoCollection<PracticeInformation> PracticeCollection => _database.GetCollection<PracticeInformation>("MedicalPractices");

        public IMongoCollection<Patient> PatientCollection => _database.GetCollection<Patient>("Patients");

    }
}
