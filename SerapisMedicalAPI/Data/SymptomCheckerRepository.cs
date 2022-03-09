using System;
using System.Collections.Generic;
using System.Linq;
using Cassandra;
using Microsoft.Extensions.Logging;
using SerapisMedicalAPI.Interfaces;
using Cassandra.Data.Linq;
using SerapisMedicalAPI.Model.Symptoms;
using SerapisMedicalAPI.Services.SymptomsChecker;

namespace SerapisMedicalAPI.Data
{
    public class SymptomCheckerRepository : ISymptomCheckerRepository
    {
        private readonly CassandraContext _cassandraContext;
        private readonly ILogger<SymptomCheckerRepository> _logger;
        private readonly ISymptomsCheckerService _symptomsCheckerService;

        public SymptomCheckerRepository(
            CassandraContext context,
            ILogger<SymptomCheckerRepository> logger,
            ISymptomsCheckerService symptomsCheckerService
            )
        {
            _logger = logger;
            _cassandraContext = context;
            _symptomsCheckerService = symptomsCheckerService;

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
            var thelist = new List<Symptoms>();
            foreach (var cRow in rows)
            {
                var id = (cRow.GetValue<int>("id"));
                var name  = cRow.GetValue<string>("name");
                thelist.Add(new Symptoms(){ID = id.ToString(), Name = name});
            }
            //var keyspaceNames = session
            //    .Execute("SELECT * FROM system_schema.keyspaces")
            //    .Select(row => row.GetValue<string>("keyspace_name"));
            return null;
        }
        public void PopulateSymptoms(IEnumerable<Symptoms> symptomsEnumerable)
        {
            try
            {
                _logger?.LogInformation("Populating "+ symptomsEnumerable.ToList().Count +" Symptoms into Cassandra ");
                var session = _cassandraContext.GetDatabaseSession;
                const string CqlQuery = "INSERT into symptoms.symptoms (id,name) values (?,?) ";
                var preparedStatement = session.Prepare(CqlQuery);
                foreach (var symptom in symptomsEnumerable)
                {
                    var boundStatement = preparedStatement.Bind(Int32.Parse(symptom.ID), symptom.Name);
                    //_logger?.LogInformation("Running Insert Statement "+boundStatement.PreparedStatement);
                    session.Execute(boundStatement);
                }
                
                _logger?.LogInformation("Done Populating Symptoms into Cassandra ");
                
                var rowSet3 = session.Execute("select * from symptoms.symptoms");
            
                var rows = rowSet3.AsEnumerable();
                var thelist = new List<Symptoms>();
                foreach (var cRow in rows)
                {
                    var id2 = (cRow.GetValue<int>("id"));
                    var name  = cRow.GetValue<string>("name");
                    thelist.Add(new Symptoms(){ID = id2.ToString(), Name = name});
                }
                _logger?.LogInformation(" Symptoms being returned is: {@thelist} ",thelist);

                
            }
            catch (Exception ex)
            {
                _logger?.LogError("Running Insert Statement "+ex);
                throw ex;
            }  
        }

        public void PopulateAllDiagnosisBySymptoms(string id)
        {
            // For every ID array combonation I need to store it in the DB for quicker respoonses
            //Eg.  IDCombo   | Description                      | Count?   
            //     234.34.11 | Diagnosis attempt for this combo | 23
            
            
        }


        public IEnumerable<DiagnosisResponse> GetProposedDiagnosisBySymptoms(string id)
        {
            _logger?.LogInformation("Populating Diagnosis"+id+"  into Cassandra ");
            var session = _cassandraContext.GetDatabaseSession;
            string CqlQuery = $"SELECT * FROM symptoms.symptoms WHERE id={id}";
            
            var rowSet = session.Execute(CqlQuery);

            if (rowSet != null)
            {
                _logger?.LogInformation("ID: "+id+" was found in Cassandra");
                int count = rowSet.First().GetValue<int>("count");
                
                string CqlQuery3 = $"UPDATE serapismedical.diagnosis_by_symptoms SET count = WHERE count ={count} ";
                session.Execute(CqlQuery3);
                return null;
            }
                
            _logger?.LogInformation("ID: "+id+" was not found, now inserting Diagnosis into Cassandra");
            const string CqlQuery2 = "INSERT into serapismedical.diagnosis_by_symptoms (id,description,count) values (id,?,?) ";
            var rowSet2 = session.Execute(CqlQuery2);
            _logger?.LogInformation("Insertion Complete!!");
            //var value = _symptomsCheckerService.GetProposedDiagnosisBySymptoms("male", "1984", arr);

            return null;
        }
    }
}