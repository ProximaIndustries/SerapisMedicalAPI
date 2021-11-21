using SerapisMedicalAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model.PatientModel;
using System.Diagnostics;

namespace SerapisMedicalAPI.Data
{
    public class PatientRepository : IPatientRepository
    {
        private readonly Context _context = null;
        public PatientRepository()
        {
            _context = new Context();
        }

        private ObjectId GetInternalId(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            try
            {
                var result = await _context.PatientCollection
                    .Find(_ => true)
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Add patient to the platform
        public async Task AddPatient(Patient _patientUser)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Remove patient from the platform
        public async Task RemovePatient(Patient _id)
        {
            try
            {
                if (_id != null)
                {
                    var filter = Builders<Patient>
                        .Filter
                        .Eq("_id", _id);

                    var result = await _context.PatientCollection.DeleteOneAsync(filter);

                    if (result.IsAcknowledged)
                    {
                        //send email to tell the customer that we have removed their profile
                    }
                    else
                    {
                        //There was error
                    }
                }
                else
                {
                    //don't do anything
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Edit patients file/information
        public async Task<bool> EditPatientUser(Patient patient)
        {
            try
            {
                if (patient != null)
                {
                    //Specifiy the filter 
                    var filter = Builders<Patient>
                        .Filter
                        .Eq(x => x.id, patient.id);

                    ReplaceOneResult updateResult = await _context.PatientCollection.ReplaceOneAsync(w => w.id.Equals(patient.id),
                            patient, new UpdateOptions { IsUpsert = true });
                    if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
                    {
                        Debug.WriteLine("Did DB update? updateResult.IsAcknowledged[ " + updateResult.IsAcknowledged + "]+updateResult.ModifiedCount[" + updateResult.ModifiedCount + "]");
                        return true; //This will trigger an success message on the Client device
                    }
                    else
                    {
                        Debug.WriteLine("DB update NO? updateResult.IsAcknowledged[ " + updateResult.IsAcknowledged + "]+updateResult.ModifiedCount[" + updateResult.ModifiedCount + "]");
                        return false; //This will trigger an unsuccesful message on the Client device
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        //Get a particular patient info
        public async Task<Patient> GetPatientDetails(ObjectId _id)
        {
            //5c0ef23c1c9d440000eee9ae

            try
            {
                if (_id != null)
                {
                    var filter = Builders<Patient>
                         .Filter
                         .Eq("firstName", _id);

                    var result = await _context.PatientCollection.Find(filter).FirstOrDefaultAsync();

                    return result;

                }
                else
                {
                    return null;
                }
            }
            catch (TimeoutException timeOut)
            {
                throw timeOut;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Patient> GetPatientById(string _id)
        {
           
                var filter = Builders<Patient>.Filter.Eq(z => z.id, _id);
                Patient _patientinfo = new Patient();
                try
                {
                    _patientinfo = await _context
                                .PatientCollection
                                .Find(filter)
                                .FirstOrDefaultAsync();

                    if (_patientinfo == null)
                    {

                    }

                }
                catch (Exception ex)
                {
                    // log or manage the exception
                    throw new Exception("Failed to Pull Patient Info: " +
                           new { _id, _patientinfo }, ex);
                }
                return _patientinfo;
        }
    }
}
