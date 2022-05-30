using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
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
                /*var session =
                    Cluster.Builder()
                        .WithCloudSecureConnectionBundle(@"\secure-connect-serapismedical.zip")
                        //or if on linux .WithCloudSecureConnectionBundle(@"/PATH/TO/>>secure-connect-serapismedical.zip")
                        .WithCredentials("ueIbwWaQCLecvNQwBNuvlbbE",
                            "UbyEl3OeSrHsmQNIJQZSKmujJ_dDKcQnmz-9XSYcxOztbIZ2fcrmqQrTmrfZ3Fmwjx-BN-lSR-cpwIf6x1gX4ZJ7sZLLieqwv6rLPDE3SDs.l,_Z86gku3aOeaIHuRPW")
                        .Build()
                        .Connect();*/
                var options = new Cassandra.SSLOptions(SslProtocols.Tls12, true, ValidateServerCertificate);
                options.SetHostNameResolver((ipAddress) => "kbalpha.cassandra.cosmos.azure.com");
                var  session = Cluster.Builder()
                    .WithCredentials("kbalpha", "Z9dhtf4k7QVH0c3i5iC34HnjP3zpTgIdtboBTafu6QQvuUC3NmQm5ilsXPP5ARCj6lFciMbtbn2ArntvQbdEHw==")
                    .WithPort(10350)
                    .AddContactPoint("kbalpha.cassandra.cosmos.azure.com")
                    .WithSSL(options)
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

        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslpolicyerrors)
        {
            //throw new NotImplementedException();
            return true;
        }

        public ISession GetDatabaseSession { get; }
    }
}
