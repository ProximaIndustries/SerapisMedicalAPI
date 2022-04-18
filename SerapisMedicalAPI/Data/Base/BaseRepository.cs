using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace SerapisMedicalAPI.Data.Base
{
    public abstract class BaseRepository<T> //: IRepositoryBase<T> where T : BaseEntity
    {
        private BaseContext<T> _context;
        public async Task<IEnumerable<T>> GetAllListItems(string collectionName)
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
                throw ex;
            }

        }
    }
}