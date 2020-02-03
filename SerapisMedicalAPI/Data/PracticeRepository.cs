using MongoDB.Bson;
using MongoDB.Driver;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.DoctorModel.Practice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Data
{
    public class PracticeRepository : IPracticeRepository
    {
        private readonly Context _context = null;

        public PracticeRepository() 
        {
            _context = new Context();
        }

        public Task AddPractice(Practice practice)
        {
            throw new NotImplementedException();
        }

        public Task<Practice> EditPracticeInfo(object _id)
        {
            throw new NotImplementedException();
        }

        public async Task<Practice> GetPracticeById(ObjectId _id)
        {
            var filter = Builders<Practice>.Filter.Eq(z => z.Id, _id);

            try
            {
                return await _context
                                .PracticeCollection
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public Task<Practice> GetPracticeDetails(object _id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Practice>> GetPractices()
        {
            //var filter = Builders<Practice>.Filter.AnyEq("DoctorAvaliable", new ObjectId ( "5bc8f2c21c9d440000a98282" ));
            //var result = await _context.PracticeCollection
            //    .Find(filter)
            //    .ToListAsync();

            var result = await _context.PracticeCollection
                .Find(_ => true).ToListAsync();

            return result;
        }

        public Task<IEnumerable<Practice>> GetPractices(object _id, double maxDistance)
        {
            throw new NotImplementedException();
        }

        public Task<Practice> RemovePractice(object _id)
        {
            throw new NotImplementedException();
        }

        Task<PracticeInformation> IPracticeRepository.GetPracticeById(ObjectId _id)
        {
            throw new NotImplementedException();
        }
    }
}
