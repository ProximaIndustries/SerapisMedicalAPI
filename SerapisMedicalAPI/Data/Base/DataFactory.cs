using SerapisMedicalAPI.Data.Supabase;
using SerapisMedicalAPI.Enums;
using SerapisMedicalAPI.Interfaces;

namespace SerapisMedicalAPI.Data.Base
{
    public class DataFactory
    {
        private readonly Context _context;

        public DataFactory(Context context)
        {
            _context = context;
        }
        IAccountRepository CreateDatasource(DataSource dataSource)
        {
            switch (dataSource)
            {
                /*case DataSource.Supabase:
                    return new AccountSupabaseRepository();*/
                default:
                    return new AccountRepository(_context);
            }
            
        }
    }
}