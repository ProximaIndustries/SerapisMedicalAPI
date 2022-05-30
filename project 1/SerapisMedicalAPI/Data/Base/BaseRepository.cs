using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;

namespace SerapisMedicalAPI.Data.Base
{
    public abstract class BaseRepository<T> //: where T : BaseEntity
    {
        private BaseContext<T> _context;

        public BaseRepository( BaseContext<T> context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllItems(string collectionName)
        {
            try
            {
                
                var result = await _context.GetCollectionByName(collectionName)
                    .Find(_ => true)
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                Log.Information(ex.ToJson());
                throw ex;
            }

        }
        
        public async Task<T> GetItemById(string collectionName, string id)
        {
            try
            {
                var result = (await _context.GetCollectionByName(collectionName)
                    .FindAsync(Builders<T>.Filter.Eq("_id", id) )).FirstOrDefault();

                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.ToJson());
                throw e;
            }
        }

        public async Task AddItem(string collectionName,  T obj)
        {
            await _context.GetCollectionByName(collectionName).InsertOneAsync(obj);
        }
        
        public async Task<bool> UpdateItem(string collectionName, string id, T obj)
        {
           var result =  await _context.GetCollectionByName(collectionName).ReplaceOneAsync(Builders<T>.Filter.Eq("_id", id), obj);

           return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}