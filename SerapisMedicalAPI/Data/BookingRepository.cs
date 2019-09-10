using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.PatientModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SerapisMedicalAPI.Data
{
    public class BookingRepository : IBookingRepository
    {
        private readonly Context _context = null;
        public List<Appointment> Updatelist = new List<Appointment>();

        //I think it's better if you work with 'BsonDocument' instead of MyType/object. 
        //If MyType has BsonIgnoreExtraFields flag, the document will ignore fields that are not represented in MyType class. In insert method,
        //   method will only insert fields that exists in class and you will lose all other nonrepresented fields.
        public BookingRepository()
        {
            _context = new Context();
        }

        public async Task<bool> AddBooking(Practice practice, Appointment booking)
        {
            List<Appointment> _medicalibuilding = new List<Appointment>();
            //
            _medicalibuilding.AddRange(practice.Appointments);
            _medicalibuilding.Add(booking);

            var filter = Builders<Practice>.Filter
                                    .Eq(x => x.Id, practice.Id);
            var update = Builders<Practice>.Update
                                    .Set(s => s.Appointments, _medicalibuilding);

            try
            {
                UpdateResult updateResult
                    = await _context
                                .PracticeCollection
                                .UpdateOneAsync(
                                        filter: filter,
                                        update: update,
                                        options: new UpdateOptions { IsUpsert = true });

                return updateResult.IsAcknowledged &&
                updateResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                //log or manage the exception
                throw ex;
            }
        }

        public Task CancelBooking(object _id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Appointment>> GetAllAvaliableBookings(IPracticeRepository maxPracticeDistance)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Appointment>> GetBookedPatientsAsync(DateTime date)
        {

            var filter = Builders<Appointment>
                        .Filter
                        .Eq("DateBooked", date);

            var result = await _context.BookingsCollection.Find(filter).ToListAsync();

            return result;
            
        }


        public async Task<bool> MakeBooking(Practice practice)
        {
          //Doesnt update
          //Dont use this method for making an appointment -in the app
          //TODO: Post data to Appointments Collection 


            var filter = Builders<Practice>.Filter
                                    .Eq(x => x.Id, practice.Id);
            var update = Builders<Practice>.Update
                                    .Set(s => s.Appointments,practice.Appointments );

            try
            {

               

                UpdateResult updateResult
                    = await _context
                                .PracticeCollection
                                .UpdateOneAsync(
                                        filter:filter,
                                        update: update,
                                        options: new UpdateOptions { IsUpsert = true});

                return updateResult.IsAcknowledged &&
                updateResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                //log or manage the exception
                throw ex;
            }

        }

        public Task PostponeBooking(object _id)
        {
            throw new NotImplementedException();
        }
    }
}
