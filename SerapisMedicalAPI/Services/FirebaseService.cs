using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Storage.v1.Data;
using Microsoft.Extensions.Logging;
using Google.Cloud.Firestore;
using Google.Cloud.Storage.V1;


namespace SerapisMedicalAPI.Services
{
    public class FirebaseService
    {
        private readonly ILogger<FirebaseService> _logger;
        
        public FirebaseService(ILogger<FirebaseService> logger)
        {
            _logger = logger;
        }


        public void AuthenticateUser()
        {
            StringBuilder sb = new StringBuilder();
            //build headers
            Dictionary<string, string> headers = new Dictionary<string, string>();

            string url = sb.ToString();
            var responseMessage = APIConector<object>.PostExternalAPIData(url,null, headers);

        }

        public async Task StoreDocument()
        {
            string project = "";
            FirestoreDb db = FirestoreDb.Create(project);
            _logger.LogInformation("Created Cloud Firestore client with project ID: {0}", project);
            
            DocumentReference docRef = db.Collection("users").Document("alovelace");
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "First", "Ada" },
                { "Last", "Lovelace" },
                { "Born", "1815" }
            };
            await docRef.SetAsync(user);
        }
        
        public Bucket CreateBucket(string projectId = "your-project-id", string bucketName = "your-unique-bucket-name")
        {
            var storage = StorageClient.Create();
            var bucket = storage.CreateBucket(projectId, bucketName);
            _logger.LogInformation($"Created {bucketName}.");
            return bucket;
        }
    }
}