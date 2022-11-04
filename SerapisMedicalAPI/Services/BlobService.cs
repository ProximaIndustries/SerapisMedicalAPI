using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SerapisMedicalAPI.Interfaces;
using System.IO;
using System.Threading.Tasks;
using Azure;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using SerapisMedicalAPI.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Azure.Core;
using System;
using SerapisMedicalAPI.Model;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using MongoDB.Bson;
using Google.Cloud.Firestore.V1;
using System.Threading;
using EllipticCurve.Utils;
using System.ComponentModel;

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

        public async Task<Stream> GetBlobAsync(string blobID, string containerID)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerID);
            BlobClient blobClient = containerClient.GetBlobClient(blobID);
            var blobDownloadInfo = await blobClient.OpenReadAsync();

            return blobDownloadInfo;
        }

        public async Task<BlobProperties> GetBlobProperties(string blobID, string ContainerID)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(ContainerID);
            BlobClient blobClient = containerClient.GetBlobClient(blobID);

            var properties = blobClient.GetProperties();

            return properties;  
        }

        public async Task<IEnumerable<string>> ListBlobsAsync(string containerID)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerID);
            var items = new List<string>();
            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                items.Add(blobItem.Name);
            }
            return items; 
        }

        public async Task<Response<BlobContentInfo>> UploadAsync(string containerID, string patientInfo)
        {
            // Create new container for 
            var containerclient = _blobServiceClient.GetBlobContainerClient(containerID);

            containerclient.CreateIfNotExists();

            //figure out what we use for  BlobName!!!

            var blobClient = containerclient.GetBlobClient(containerID);

            var bytes = Encoding.UTF8.GetBytes(patientInfo);

            await using var memoryStream = new MemoryStream(bytes);

            return await blobClient.UploadAsync(memoryStream);

        }
    }
}