using SerapisMedicalAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Helpers;
using SerapisMedicalAPI.Helpers.Validations;
using SerapisMedicalAPI.Utils;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace SerapisMedicalAPI.Data
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly Context _context;
        private Stopwatch timer = new Stopwatch();
        private readonly ILogger<DoctorRepository> _logger;
        public DoctorRepository(Context context, ILogger<DoctorRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        private ObjectId GetId(string id)
        {
            ObjectId Id;
            if (!ObjectId.TryParse(id, out Id))
                Id = ObjectId.Empty;

            return Id;
        }

        //Just as an example: to get all the Doctors in DB, we make an async request
        public async Task<IEnumerable<Doctor>> GetAllDoctor()
        {
            try
            {
                timer.Start();
                
                var result = await _context.DoctorCollection
                    .Find(_ => true)
                    .ToListAsync();

                timer.Stop();
                _logger?.LogInformation("The Response Time of MongoDriver call took [ " + timer.ElapsedMilliseconds + "ms" + " ]");
                timer.Reset();
                return result;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }

        }

        public async Task AddDoctor(Doctor doc)
        {
            try
            {
                timer.Start();
                await _context
                    .DoctorCollection
                    .InsertOneAsync(doc);
                timer.Stop();
                _logger?.LogInformation("The Response Time of MongoDriver call took [ " + timer.ElapsedMilliseconds + "ms" + " ]");
                timer.Reset();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task<Doctor> GetDoctor(string privateid)
        {
            try
            {
                timer.Start();
                ObjectId Id = GetId(privateid);
                var result = await _context.DoctorCollection
                    .Find(doctor => doctor.User.AuthId == privateid)
                    .FirstOrDefaultAsync();
                timer.Stop();
                _logger?.LogInformation("The Response Time of MongoDriver call took [ " + timer.ElapsedMilliseconds + "ms" + " ]");
                timer.Reset();
                return result;
            }
            catch (Exception ex)
            {
                //log or manage the exception
                throw ex;
            }
        }

        public async Task<Doctor> AuthenticateDoctor(string _id, string password)
        {   
            // _id can either be an email address or and MP number
            Doctor AuthenticatedRegisteredDoctor;
            try
            {
                // Validate Health ID from department of Health
                 
                //EncryptService _securehash = new EncryptService();
                // string SecurePassword = _securehash.DecryptCipherTextToPlainText(password);
                if (!StringUtils.IsValidEmail(_id))
                {
                    if (!DoctorValidation.isValidMPNumber(_id))
                        return null;
                    //hash password
                   


                    // Check if doctor exists
                    var filter = Builders<Doctor>.Filter.Eq(doc => doc.User.HealthId, _id) & Builders<Doctor>.Filter.Eq(doc => doc.User.Password, password);

                    AuthenticatedRegisteredDoctor = await _context.DoctorCollection
                                                          .Find(filter)
                                                          .FirstOrDefaultAsync();
                    //Return null if the doctor isnt registered with us
                    //we need to handle this null return
                    if (AuthenticatedRegisteredDoctor == null)
                        return null;


                    //if doctor pass "exist check"
                    //Password check if correct
                    //Need to set token to expire after 12 hours

                    //return the registered user
                    return AuthenticatedRegisteredDoctor;
                }
                else
                {
                    var filter = Builders<Doctor>.Filter.Eq(doc => doc.User.Email, _id) & Builders<Doctor>.Filter.Eq(doc => doc.User.Password, password);

                    AuthenticatedRegisteredDoctor = await _context.DoctorCollection
                                                          .Find(filter)
                                                          .FirstOrDefaultAsync();
                    //Return null if the doctor isnt registered with us
                    //we need to handle this null return
                    if (AuthenticatedRegisteredDoctor == null)
                        return null;


                    //if doctor pass "exist check"
                    //Password check if correct
                    //Need to set token to expire after 12 hours

                    //return the registered user
                    return AuthenticatedRegisteredDoctor;
                }
                
                //Send SMS


            }catch(Exception ex)
            {
                Debug.WriteLine(ex);            
            }

            return null;
        }

        //the IsAcknowledged and ModifiedCount properties, this is how MongoDB keep track of changes.
        //When doing operations such as, 'ReplaceOneAsync()' and 'DeleteOneAsync()', an object is returned, 
        //with this object we can know the database is acknowledge and the amount of elements modified or deleted.
        //We can use this information to identify the success or fail of our operation.

        public async Task<bool> EditDoctor(Doctor doctor)
        {
            ReplaceOneResult replaceOne =
               await _context.DoctorCollection.ReplaceOneAsync(
                    filter: d => d.User.HealthId == doctor.User.HealthId,
                    replacement: doctor
                    );

            return replaceOne.IsAcknowledged && replaceOne.ModifiedCount > 0;
        }

        public Task<Doctor> RemoveDoctor(ObjectId _id)
        {
            throw new NotImplementedException();
        }

        public Task<Doctor> EditDoctor(ObjectId _id)
        {
            throw new NotImplementedException();
        }
    }
}


