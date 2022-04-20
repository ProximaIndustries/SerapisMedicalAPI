using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace SerapisMedicalAPI
{
    public class CassandraContext
    {
        private ILogger<CassandraContext> _logger;

        public CassandraContext(ILogger<CassandraContext> logger)
        {
            _logger = logger;
            
            _logger?.LogInformation("Attempting to connect to the Cassandra Cluster");
            try
            {
                var session =
                    Cluster.Builder()
                        .WithCloudSecureConnectionBundle(@"\secure-connect-serapismedical.zip")
                        //or if on linux .WithCloudSecureConnectionBundle(@"/PATH/TO/>>secure-connect-serapismedical.zip")
                        .WithCredentials("ueIbwWaQCLecvNQwBNuvlbbE",
                            "UbyEl3OeSrHsmQNIJQZSKmujJ_dDKcQnmz-9XSYcxOztbIZ2fcrmqQrTmrfZ3Fmwjx-BN-lSR-cpwIf6x1gX4ZJ7sZLLieqwv6rLPDE3SDs.l,_Z86gku3aOeaIHuRPW")
                        .Build()
                        .Connect();

                _logger?.LogInformation("Server Name:  "+ session.Cluster.Metadata.ClusterName.ToJson());
                _logger?.LogInformation("Server Hosts:  "+ session.Cluster.Metadata.AllHosts()?.ToJson());
                _logger?.LogInformation("Server Replica's :  "+ session.Cluster.Metadata.AllReplicas()?.ToJson());
                _logger?.LogInformation("Session Name:  "+ session.SessionName);
            
                GetDatabaseSession = session;
            }
            catch (Exception e)
            {
                _logger?.LogError("Cassandra Error :{0}",e);
                throw e;
            }
           

        }

        public ISession GetDatabaseSession { get; }
    }
}
