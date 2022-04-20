using System;
using System.Collections.Generic;
using System.Linq;
using Cassandra;
using Microsoft.Extensions.Logging;
using SerapisMedicalAPI.Interfaces;
using Cassandra.Data.Linq;
using Cassandra.Mapping;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using Newtonsoft.Json;
using SerapisMedicalAPI.Model.DAO;
using SerapisMedicalAPI.Model.Symptoms;
using SerapisMedicalAPI.Services.SymptomsChecker;
using Serilog;

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
            return thelist[0];
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


        public IEnumerable<DiagnosisResponse> GetProposedDiagnosisBySymptoms(string id,string age, string sex)
        {
            try
            {
                var session = _cassandraContext.GetDatabaseSession;
                IMapper mapper = new Mapper(session);
                var YearOfBirth = DateTime.Today.Year - Int32.Parse(age);
                var queryId = sex + "/" + id + "/" + age;
                string CqlQuery = $"SELECT * FROM serapismedical.diagnosis_by_symptoms WHERE diagnosis_id=\'{queryId}\'";
                _logger.LogInformation("Prepared statement  :{0}",CqlQuery);
                var obj = mapper.FirstOrDefault<DiagnosisDAO>(CqlQuery);
                


                if (obj is not null)
                {
                    _logger.LogInformation("ID: "+id+" was found in Cassandra");
                    
                    int count;

                    count = obj.diagnosis_count + 1;
                    var ps = session.Prepare( "UPDATE serapismedical.diagnosis_by_symptoms SET diagnosis_count =? WHERE diagnosis_id =? ");
                    var statement = ps.Bind(count, "id");
                    session.Execute(statement);
                    Log.Information("Incremented Count of Diagnosis to :{0}",count);
                    
                    var jsonObject = JsonConvert.DeserializeObject<IEnumerable<DiagnosisResponse>>(obj.diagnosis_description);
                    
                    return jsonObject;
                }
                
            
                _logger?.LogInformation("ID: "+queryId+" was not found, calling Symptoms Checker API");
                //5-2-1
                var arrStrings = id.Split("/");
                var strings = arrStrings[0].Split("-").ToArray();
                
                var arr = Array.ConvertAll(strings, int.Parse);
                var diagnosisResponse = _symptomsCheckerService.GetProposedDiagnosisBySymptoms(sex, YearOfBirth.ToString(), arr);
           
                //var ps = session.Prepare("UPDATE user_profiles SET birth=? WHERE key=?");
                string CqlQuery2 = "INSERT into serapismedical.diagnosis_by_symptoms (diagnosis_id,diagnosis_description,diagnosis_count) values (?,?,?) ";
                var boundStatement = session.Prepare(CqlQuery2).Bind(id,JsonConvert.SerializeObject( diagnosisResponse),1);
            
                var rowSet2 = session.Execute(boundStatement);
                _logger?.LogInformation("Insertion Complete!!");
                return diagnosisResponse;
            }
            catch (Exception e)
            {
                _logger?.LogError("StackTrace: {0} - Message:   {1}  \n Error:{3}",e.StackTrace, JsonConvert.SerializeObject(e.Message), e);
            }

            return null;
        }
    }
}