using SerapisMedicalAPI.Interfaces;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.PatientModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using SerapisMedicalAPI.Model.DoctorModel.Practice;
using SerapisMedicalAPI.Model.AppointmentModel;
using System.Diagnostics;

namespace SerapisMedicalAPI.Data
{
    public class BookingRepository : IBookingRepository
    {
        private readonly Context _context = null;
        public List<AppointmentDao> Updatelist = new List<AppointmentDao>();

        //I think it's better if you work with 'BsonDocument' instead of MyType/object. 
        //If MyType has BsonIgnoreExtraFields flag, the document will ignore fields that are not represented in MyType class. In insert method,
        //   method will only insert fields that exists in class and you will lose all other nonrepresented fields.
        public BookingRepository()
        {
            _context = new Context();
        }
         //using (var session = mongoClient.StartSession())
        public async Task<bool> AddBooking(ObjectId practiceid, AppointmentDto booking)
        {
            
            
            try
            {
                AppointmentDao _appointment = new AppointmentDao
                {
                    BookingId = ObjectId.Parse(booking.BookingId),
                    LineNumber = booking.LineNumber,
                    PatientID = ObjectId.Parse(booking.PatientID),
                    DateAndTimeOfAppointment = booking.DateAndTimeOfAppointment,
                    HasSeenGP = booking.HasSeenGP,
                    IsSerapisBooking = booking.IsSerapisBooking,
                    HasBeenToThisPractice = booking.HasBeenToThisPractice,
                    DoctorsId = ObjectId.Parse(booking.DoctorsId),
                    PracticeID = ObjectId.Parse(booking.PracticeID)
                };


                    var filter = Builders<PracticeInformation>.Filter
                                        .Eq(x => x.Id, practiceid);

                var updatev2 = Builders<PracticeInformation>.Update.Push<AppointmentDao>(e => e.Appointment, _appointment);
                UpdateResult updateResult
                    = await _context
                                .PracticeCollection
                                .UpdateOneAsync(
                                        filter: filter,
                                        update: updatev2,
                                        options: new UpdateOptions { IsUpsert = true });
                                        
                // if modifed document is equal to 1 or more then that means the document was updated
                if( updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
                {
                    Debug.WriteLine("Did DB update? [" + updateResult.IsAcknowledged + "]+ How many Documents updated [" + updateResult.ModifiedCount + "]");
                    return true; //This will trigger an success message on the Client device
                }
                else
                {
                    Debug.WriteLine("DB Didn't update  updateResult.IsAcknowledged[ " + updateResult.IsAcknowledged + "]+updateResult.ModifiedCount[" + updateResult.ModifiedCount + "]");
                    return false; //This will trigger an unsuccesful message on the Client device
                }



            }
            catch (Exception ex)
            {
                //log or manage the exception
                Debug.WriteLine(ex);
                throw ex;
                
            }
        }

        public async Task<bool> AddBookingv2(ObjectId practiceid, AppointmentDto booking)
        {

            try
            {
                using( var session = _context.MongoClient.StartSession())
                {
                    //AUTOMAPPER REQUIRED
                    AppointmentDao _appointment = new AppointmentDao
                    {
                        BookingId = ObjectId.Parse(booking.BookingId),
                        LineNumber = booking.LineNumber,
                        PatientID = ObjectId.Parse(booking.PatientID),
                        DateAndTimeOfAppointment = booking.DateAndTimeOfAppointment,
                        HasSeenGP = booking.HasSeenGP,
                        IsSerapisBooking = booking.IsSerapisBooking,
                        HasBeenToThisPractice = booking.HasBeenToThisPractice,
                        DoctorsId = ObjectId.Parse(booking.DoctorsId),
                        PracticeID = ObjectId.Parse(booking.PracticeID)
                    };

                    session.StartTransaction();

                    //Make First Update
                    var filter = Builders<PracticeInformation>.Filter
                                        .Eq(x => x.Id, practiceid);
                    var updatev2 = Builders<PracticeInformation>
                                        .Update.Push<AppointmentDao>(e => e.Appointment, _appointment);

                    UpdateResult updateResult
                            = await _context
                                        .PracticeCollection
                                        .UpdateOneAsync(session,
                                                filter: filter,
                                                update: updatev2,
                                                options: new UpdateOptions { IsUpsert = true });
                    //Make Second Update
                    var filter2 = Builders<PracticeInformation>.Filter
                                        .Eq(x => x.Id, ObjectId.Parse(booking.PatientID));
                    /*var update2 = Builders<AppointmentDao>
                                        .Update.Push(booking);*/

                    // This step will be skipped if we throw an exception
                    session.CommitTransaction();

                }
            }
            catch (Exception ex)
            {
                //log or manage the exception
                Debug.WriteLine(ex);
                throw ex;

            }
            return false;
        }


        public Task CancelBooking(object _id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AppointmentDao>> GetAllAvaliableBookings(IPracticeRepository maxPracticeDistance)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppointmentDao>> GetBookedPatientsAsync(DateTime date)
        {

            var filter = Builders<AppointmentDao>
                        .Filter
                        .Eq("DateBooked", date);

            var result = await _context.BookingsCollection.Find(filter).ToListAsync();

            return result;
            
        }


        public async Task<bool> MakeBooking(PracticeInformation practice)
        {
          //Doesnt update
          //Dont use this method for making an appointment -in the app
          //TODO: Post data to Appointments Collection 


            var filter = Builders<PracticeInformation>.Filter
                                    .Eq(x => x.Id, practice.Id);
            var update = Builders<PracticeInformation>.Update
                                    .Set(s => s.Appointment,practice.Appointment);

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
