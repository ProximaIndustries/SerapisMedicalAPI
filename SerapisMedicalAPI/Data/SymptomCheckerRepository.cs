using System;
using System.Collections.Generic;
using System.Linq;
using Cassandra;
using Microsoft.Extensions.Logging;
using SerapisMedicalAPI.Interfaces;
using Cassandra.Data.Linq;
using SerapisMedicalAPI.Model.Symptoms;

namespace SerapisMedicalAPI.Data
{
    public class SymptomCheckerRepository : ISymptomCheckerRepository
    {
        private readonly CassandraContext _cassandraContext;
        private readonly ILogger<SymptomCheckerRepository> _logger;
        public SymptomCheckerRepository(CassandraContext context, ILogger<SymptomCheckerRepository> logger)
        {
            _logger = logger;
            _cassandraContext = context;
            
            
        }

        public IEnumerable<Symptoms> GetAllSymptoms()
        {
            var session = _cassandraContext.GetDatabaseSession;

            var rowSet = session.Execute("select * from symptoms.symptoms").AsEnumerable();
            //var rows = rowSet.AsEnumerable();
            foreach (var row in rowSet)
            {
                Console.WriteLine("ID: "+row.GetColumn("id") + " Name: " +row.GetColumn("name") );
            }
            return null;
        }


        public Symptoms GetSymptomById()
        {
            var session =  _cassandraContext.GetDatabaseSession;
            
            var rowSet = session.Execute("select * from symptoms.symptoms");

            Console.WriteLine(rowSet.First().GetValue<int>("id"));
            var rows = rowSet.AsEnumerable();
            
            //var keyspaceNames = session
            //    .Execute("SELECT * FROM system_schema.keyspaces")
            //    .Select(row => row.GetValue<string>("keyspace_name"));
            return null;
        }
        public void PopulateSymptoms(IEnumerable<Symptoms> symptomsEnumerable)
        {
            try
            {

                var session = _cassandraContext.GetDatabaseSession;
                const string CqlQuery = "INSERT into symptoms.symptoms (id,name) values (?,?) ";
                var preparedStatement = session.Prepare(CqlQuery);
                foreach (var symptom in symptomsEnumerable)
                {
                    var boundStatement = preparedStatement.Bind(null, null);
                    session.Execute(boundStatement);
                }
                
               
                

                
            }
            catch (Exception ex)
            {
            }  
        }
    }
}