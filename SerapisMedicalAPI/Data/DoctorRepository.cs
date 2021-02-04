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
namespace SerapisMedicalAPI.Data
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly Context _context = null;
        DoctorValidation _doctorValidation = new DoctorValidation();
        public DoctorRepository()
        {
            _context = new Context();
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
                var result = await _context.DoctorCollection
                    .Find(_ => true)
                    .ToListAsync();

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
                await _context
                    .DoctorCollection
                    .InsertOneAsync(doc);
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
                ObjectId Id = GetId(privateid);
                var result = await _context.DoctorCollection
                    .Find(doctor => doctor.PrivateId == privateid 
                    || doctor.PrivateId ==Convert.ToString(Id))
                    .FirstOrDefaultAsync();

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
            try
            {
                // Validate Health ID from department of Health
               
                if (!_doctorValidation.isValidMPNumber(_id))
                    return null;
                //hash password
                EncryptService _securehash = new EncryptService();
               // string SecurePassword = _securehash.DecryptCipherTextToPlainText(password);
                
                
                // Check if doctor exists
                var filter = Builders<Doctor>.Filter.Eq(doc => doc.User.HealthId, _id);

                var RegisteredDoctor = await _context.DoctorCollection
                                                      .Find(filter)
                                                      .FirstOrDefaultAsync();
                //Return null if the doctor isnt registered with us
                //we need to handle this null return
                if (RegisteredDoctor == null)
                    return null;


                //if doctor pass "exist check"
                //Password check if correct
                var filter2 = Builders<Doctor>.Filter.Eq(doc => doc.User.Password, password);

                var AuthenticatedRegisteredDoctor = await _context.DoctorCollection
                                                                  .Find(filter)
                                                                  .FirstOrDefaultAsync();
                //Need to set token to expire after 12 hours

                //return the registered user
                return AuthenticatedRegisteredDoctor;
                //Send SMS



            }catch(Exception ex)
            {

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


