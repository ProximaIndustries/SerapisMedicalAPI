using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using SerapisMedicalAPI.Interfaces;
using System.IO;
using System.Threading.Tasks;
using Azure;
using System.Collections.Generic;
using System;
using SerapisMedicalAPI.Model;
using Newtonsoft.Json;
using Serilog;

namespace SerapisMedicalAPI.Services
{ 
    //implement interface and add missing/unimplemented members
    public class BlobService : IBlobStorage
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }


        public async Task DeleteBlobAsync(string blobID, string containerID)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerID);
            var blobClient = containerClient.GetBlobClient(blobID);
            await blobClient.DeleteIfExistsAsync();
        }
        

        public async Task<byte[]> GetBlobAsync(string blobID, string containerID)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerID);
            BlobClient blobClient = containerClient.GetBlobClient(blobID);
            var blobDownloadInfo = await blobClient.OpenReadAsync();
            using var memoryStream = new MemoryStream();
            await blobDownloadInfo.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        public async Task<BlobProperties> GetBlobProperties(string blobID, string ContainerID)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(ContainerID);
            BlobClient blobClient = containerClient.GetBlobClient(blobID);

            var properties = await blobClient.GetPropertiesAsync();

            return properties;
        }

        public async Task<IEnumerable<string>> ListBlobsAsync(string containerID)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerID);
            var items = new List<string>();
            await foreach (var blobItem in containerClient.GetBlobsAsync().ConfigureAwait(false))
            {
                items.Add(blobItem.Name);
            }

            return items;
        }

        public async Task<Response<BlobContentInfo>> UploadAsync(string containerID, StorageObject patientInfo)
        {
            // Create new container for 
            var containerclient = _blobServiceClient.GetBlobContainerClient(containerID);
            await containerclient.CreateIfNotExistsAsync();

            //figure out what we use for  BlobName!!!
            if (patientInfo.Data.StartsWith("data"))
            {
                var x = patientInfo.Data[(patientInfo.Data.LastIndexOf(',') + 1)..];
                patientInfo.Data = x;
            }

            var blobClient = containerclient.GetBlobClient(containerID);
            var bytes = Convert.FromBase64String($@"{patientInfo}"); // without data:image/jpeg;base64 prefix, just base64 string
            var header = new BlobHttpHeaders();
            header.ContentType = patientInfo.ContentType;
            await using var memoryStream = new MemoryStream(bytes);

            var response = await blobClient.UploadAsync(memoryStream,header);
            Log.Information("UploadAsync Response : {Response}", JsonConvert.SerializeObject(response));
            return response;
        }
    }
}