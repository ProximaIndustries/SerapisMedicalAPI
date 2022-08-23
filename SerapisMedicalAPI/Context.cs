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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SerapisMedicalAPI.Model.Bookings;
using Serilog;

namespace SerapisMedicalAPI
{
    public class Context
    {
        private readonly IMongoDatabase _database;
        private readonly ILogger<Context> _logger; 
        private readonly IConfiguration _config;
        
        
        
        public const string OtherConnectionString = "mongodb+srv://KhanyiTheGreat:Langelihle1!@cluster0-i3gjx.azure.mongodb.net/SerapisMedical?retryWrites=true";
        public const string ConnectionString1 = "mongodb+srv://Bonga:Langelihle1!@cluster0.bkjo1.mongodb.net/SerapisMedical?retryWrites=true&w=majority";
        public const string ConnectionString = "mongodb+srv://dbuser:pinetownEngineers1@cluster0.2sdsglx.mongodb.net/?retryWrites=true&w=majority";

        public const string ClickATell_APIKEY = "CGAmhjXETkq4ZP7DVPkxQQ==";
        public const string ClickATell_APIID = "ff8080817764737801776b66883b1230";								

        public Context(ILogger<Context> logger, IConfiguration config )
        { 
            _logger = logger;
            _config = config;
            
            //var client = new MongoClient(_config.GetValue<string>("MongoConnection:ConnectionString"));
            
            var client = new MongoClient(ConnectionString);
            if (client != null)
            {

                Log.Information("Attempting to connect to the Database");
                _logger?.LogInformation("Pool of Servers avaliable:  " + client.Cluster.Description.Servers.Count() );
                _logger?.LogInformation("Server State:  " + client.Cluster.Description.State );
                if(!"Disconnected".Equals (client.Cluster.Description.State.ToString()))
                {
                    _logger?.LogInformation("Server Region [" + client?.Cluster?.Description?.Servers[0].Tags?.Tags[0] + "] Server Provider [" + client.Cluster?.Description?.Servers?[0].Tags?.Tags[1] + "]"); //0 == region 1 == provider
                }
                _logger?.LogInformation("Attempting to connect to the Database");
                _database = client.GetDatabase("SerapisMedical");
                _logger?.LogInformation("Server State:  " + client.Cluster.Description.State);
                _logger?.LogInformation("Database Connected To :" + _database.DatabaseNamespace.DatabaseName);
            }
            else
            {
                Exception ex = new Exception();
                _logger?.LogError("Failed to connect to the Database", ex);
                Debug.WriteLine(ex.ToString());
                throw ex;
               
            }
        }


        public IMongoClient MongoClient;
        public IMongoCollection<Doctor> DoctorCollection => _database.GetCollection<Doctor>("Users");
        
        //lambda way to do it
        //public IMongoCollection<AppointmentDao> BookingsCollection => _database.GetCollection<AppointmentDao>("PatientBookings");

        public IMongoCollection<AppointmentDao> ApppointmentCollection => _database.GetCollection<AppointmentDao>("Appointments");
        public IMongoCollection<PracticeInformation> PracticeCollection => _database.GetCollection<PracticeInformation>("MedicalPractices");
        public IMongoCollection<Booking> BookingsCollection => _database.GetCollection<Booking>("Bookings2");

        public IMongoCollection<Patient> PatientCollection => _database.GetCollection<Patient>("Patients");
        public IMongoCollection<Patient> SMSCollection => _database.GetCollection<Patient>("SMS");
    }
}
