using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private Apimedic apimedic = new Apimedic();

        public SymptomCheckerRepository(
            CassandraContext context,
            ILogger<SymptomCheckerRepository> logger,
            ISymptomsCheckerService symptomsCheckerService
        )
        {
            _logger = logger;
            _cassandraContext = context;
            _symptomsCheckerService = symptomsCheckerService;
            _symptomsCheckerService = apimedic;
            //_symptomsCheckerService = symptomsCheckerService as Apimedic;

        }

        public async Task<IEnumerable<Symptoms>> GetAllSymptoms()
        {
            List<Symptoms> symptomsList = new List<Symptoms>();
            var session = _cassandraContext.GetDatabaseSession;
            /*var rowSet = session.Execute("select * from symptoms.symptoms").AsEnumerable();
            //var rows = rowSet.AsEnumerable();
            foreach (var row in rowSet)
            {
                Console.WriteLine("ID: "+row.GetColumn("id") + " Name: " +row.GetColumn("name") );
            }*/
            var rowSet = session.Execute("select * from serapismedical.symptoms");
            //_logger?.LogInformation("Grabbing Symptoms from APIMEDIC ");

            foreach (var row in rowSet)
            {
                var symptom = new Symptoms()
                {
                    ID = row.GetValue<int>("id"),
                    Name = row.GetValue<string>("name")
                };
                symptomsList.Add(symptom);
            }
            //var listOfSymptoms = rowSet.GetRows(); 
                //await _symptomsCheckerService.GetAllSymptoms();
            //await PopulateSymptoms(listOfSymptoms);
            return symptomsList;
        }
        

        public Symptoms GetSymptomById()
        {
            var session =  _cassandraContext.GetDatabaseSession;
            
            var rowSet = session.Execute("select * from serapismedical.symptoms");
            if (!rowSet.Any()) return new Symptoms();
            Console.WriteLine(rowSet.First().GetValue<int>("id"));
            var rows = rowSet.AsEnumerable();
            var thelist = (from cRow in rows let id = cRow.GetValue<int>("id") 
                let name = cRow.GetValue<string>("name") 
                select new Symptoms() {ID = id, Name = name}).ToList();

            return thelist[0];
        }
        public async Task PopulateSymptoms(IEnumerable<Symptoms> symptomsEnumerable)
        {
            try
            {
                _logger?.LogInformation("Populating "+ symptomsEnumerable.ToList().Count +" Symptoms into Cassandra ");
                var session = _cassandraContext.GetDatabaseSession;
                const string CqlQuery = "INSERT into serapismedical.symptoms (id,name) values (?,?) ";
                var preparedStatement = session.Prepare(CqlQuery);
                foreach (var symptom in symptomsEnumerable)
                {
                    var boundStatement = preparedStatement.Bind(symptom.ID, symptom.Name);
                    _logger?.LogInformation("Inserting "+symptom.ID+" : "+ symptom.Name);
                    session.Execute(boundStatement);
                }
                
                _logger?.LogInformation("Done Populating Symptoms into Cassandra ");
                
                var rowSet3 = session.Execute("select * from serapismedical.symptoms");
            
                var rows = rowSet3.AsEnumerable();
                var thelist = new List<Symptoms>();
                foreach (var cRow in rows)
                {
                    var id2 = (cRow.GetValue<int>("id"));
                    var name  = cRow.GetValue<string>("name");
                    thelist.Add(new Symptoms(){ID = id2, Name = name});
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
                


                if (obj != null)
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
                var strings = arrStrings[0].Split(",").ToArray();
                
                var arr = Array.ConvertAll(strings, int.Parse);
                var diagnosisResponse = _symptomsCheckerService.GetProposedDiagnosisBySymptoms(sex, age, arr).Result;
           
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