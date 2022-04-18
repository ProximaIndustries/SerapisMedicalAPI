using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace SerapisMedicalAPI.Data.Base
{
    public class BaseContext<T>
    {
        private readonly IMongoDatabase _database;
        private readonly ILogger<BaseContext<T>> _logger; 
        private readonly IConfiguration _config;
        
        
        
        public const string OtherConnectionString = "mongodb+srv://KhanyiTheGreat:Langelihle1!@cluster0-i3gjx.azure.mongodb.net/SerapisMedical?retryWrites=true";
        public const string ConnectionString = "mongodb+srv://Bonga:Langelihle1!@cluster0.bkjo1.mongodb.net/SerapisMedical?retryWrites=true&w=majority";

        public const string ClickATell_APIKEY = "AfMDkjnITRaKOQKiV6mN_g==";
        public const string ClickATell_APIID = "ff8080817764737801776b66883b1230";								

        public BaseContext(ILogger<BaseContext<T>> logger, IConfiguration config )
        { 
            _logger = logger;
            _config = config;
            
            //var client = new MongoClient(_config.GetValue<string>("MongoConnection:ConnectionString"));
            
            var client = new MongoClient(ConnectionString);
            if (client != null)
            {

                Log.Information("Attempting to connect to the Database");
                _logger?.LogInformation("Pool of Servers avaliable:  " + client.Cluster.Description.Servers.Count() );
                _logger?.LogInformation("Server State:  " + client.Cluster.Description.State );
                if(!"Disconnected".Equals (client.Cluster.Description.State.ToString()))
                {
                    _logger?.LogInformation("Server Region [" + client.Cluster.Description.Servers[0].Tags.Tags[0] + "] Server Provider [" + client.Cluster.Description.Servers[0].Tags.Tags[1] + "]"); //0 == region 1 == provider
                }
                _logger?.LogInformation("Attempting to connect to the Database");
                _database = client.GetDatabase("SerapisMedical");
                _logger?.LogInformation("Server State:  " + client.Cluster.Description.State);
                _logger?.LogInformation("Database Connected To :" + _database.DatabaseNamespace.DatabaseName);
            }
            else
            {
                Exception ex = new Exception();
                _logger?.LogError("Failed to connect to the Database", ex);
                Debug.WriteLine(ex.ToString());
                throw ex;
               
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collectionName">Input the Name of the collection you are trying to transact with</param>
        /// <returns>Returns The collection of IMongoCollection<T> </returns>
        public IMongoCollection<T> GetCollectionByName (string collectionName) => _database.GetCollection<T>(collectionName);
            
    }
}