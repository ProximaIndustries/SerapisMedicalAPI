using System.Reflection.Metadata;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using SerapisMedicalAPI.Helpers;
using SerapisMedicalAPI.Model;
using SerapisMedicalAPI.Model.DoctorModel.Practice;
using SerapisMedicalAPI.Model.PracticeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Data
{
    public class PracticeRepository : IPracticeRepository
    {
        private int attempt { get; set; }
        private const int  RETRY_VALUE = 3;

        private readonly Context _context = null;

        private List<PracticeDto> practiceDtoList = new List<PracticeDto>();

        public PracticeRepository() 
        {
            _context = new Context();
        }

        public Task AddPractice(PracticeInformation practice)
        {
            throw new NotImplementedException();
        }

        public Task<PracticeInformation> EditPracticeInfo(object _id)
        {
            throw new NotImplementedException();
        }

        public Task<PracticeInformation> EditPracticeInfo(ObjectId _id)
        {
            throw new NotImplementedException();
        }

        public async Task<PracticeInformation> GetPracticeById(ObjectId _id)
        {
            var filter = Builders<PracticeInformation>.Filter.Eq(z => z.Id, _id);
            PracticeInformation _practiceinfo = new PracticeInformation();
            try
            { 
                _practiceinfo = await _context
                            .PracticeCollection
                            .Find(filter)
                            .FirstOrDefaultAsync();

                if(_practiceinfo == null)
                {
                    
                }
                  
            }
            catch(Exception ex)
            {
                // log or manage the exception
                 throw new Exception("Failed to Pull Practice Info: " + 
                        new{ _id, _practiceinfo }, ex);
            }
            return _practiceinfo;
        }
        public async Task<PracticeInformation> GetPracticeIfDoctorWorksThere(ObjectId _id)
        {
            var filter = Builders<PracticeInformation>.Filter.Eq(z => z.Id, _id);
            PracticeInformation _practiceinfo = new PracticeInformation();
            try
            { 
                _practiceinfo = await _context
                            .PracticeCollection
                            .Find(filter)
                            .FirstOrDefaultAsync();

                if(_practiceinfo == null)
                    return _practiceinfo;
                
                
                
            }
            catch(Exception ex)
            {
                // log Ex
                
                // Manage the exception
                 throw new Exception("Failed to Pull Doctors Info: " + 
                        new{ _id, _practiceinfo }, ex);
            }
            return _practiceinfo;
        }

        public Task<PracticeInformation> GetPracticeDetails(object _id)
        {
            throw new NotImplementedException();
        }

        public Task<PracticeInformation> GetPracticeDetails(ObjectId _id)
        {
            PracticeInformation practiceInformation = new PracticeInformation();

            var builder = Builders<PracticeInformation>.Filter;

            //var filter = builder.Eq<PracticeInformation>(x=>x.Id, _id);

            //practiceInformation = _context.PracticeCollection.Find(filter).FirstOrDefault();

            return Task.FromResult(practiceInformation);
        }

        public async Task<IEnumerable<PracticeDto>> GetPractices()
        {
            //var filter = Builders<Practice>.Filter.AnyEq("DoctorAvaliable", new ObjectId ( "5bc8f2c21c9d440000a98282" ));
            //var result = await _context.PracticeCollection
            //    .Find(filter)
            //    .ToListAsync();

            practiceDtoList.Clear();

            var result = await _context.PracticeCollection
                .Find(_ => true).ToListAsync();

            foreach (var _practice in result)
            {
                practiceDtoList.Add(DtoCreator.CreatePracticeDto(_practice));
            }

            return practiceDtoList;
        }

        public Task<IEnumerable<PracticeDto>> GetPractices(double lat, double lon, double maxDistance)
        {
            practiceDtoList.Clear();

            var point = GeoJson.Point(GeoJson.Geographic(lon, lat));

            FieldDefinition<PracticeInformation> fieldDefinition;

            //THE COMMENTED CODE IS TO FILTER BY CO-ORDINATES, YOU MULTIPLY BY 1000 TO CONVERT TO KILOMETERS

            //var filter = Builders<PracticeInformation>.Filter.Near(fieldDefinition, point, maxDistance*1000, 1000);

            //practiceDtoList=_context.AnyEq<>()

            //return Task.FromResult(practiceDtoList);

            throw new NotImplementedException();
        }


        public Task<PracticeInformation> RemovePractice(ObjectId _id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<PracticeInformation>> IPracticeRepository.GetPractices()
        {
            throw new NotImplementedException();
        }
    }
}
