﻿using SerapisMedicalAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using SerapisMedicalAPI.Interfaces;

namespace SerapisMedicalAPI.Data
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly Context _context = null;

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

        public Task<Doctor> GetDoctor(ObjectId _id)
        {
            throw new NotImplementedException();
        }

        //the IsAcknowledged and ModifiedCount properties, this is how MongoDB keep track of changes.
        //When doing operations such as, 'ReplaceOneAsync()' and 'DeleteOneAsync()', an object is returned, 
        //with this object we can know the database is acknowledge and the amount of elements modified or deleted.
        //We can use this information to identify the success or fail of our operation.

        public async Task<bool> EditDoctor(Doctor doctor)
        {
            ReplaceOneResult replaceOne =
               await _context.DoctorCollection.ReplaceOneAsync(
                    filter: d => d.User.UserId == doctor.User.UserId,
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


