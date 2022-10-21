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


        public async Task DeleteBlobAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("medicaldocuments");
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<Stream> GetBlobAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("medicaldocuments");
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            var blobDownloadInfo = await blobClient.OpenReadAsync();

            return blobDownloadInfo;
        }

        public async Task<IEnumerable<string>> ListBlobsAsync()
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("medicaldocuments");

            var items = new List<string>();

            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                items.Add(blobItem.Name);
            }

            return items; 
        }

        public async Task<Task<Response<BlobContentInfo>>> UploadAsync(string blob, string FileName)
        {
            // Create new upload response object that we can return to the requesting method
           

            // container client
            var containerclient = _blobServiceClient.GetBlobContainerClient("medicaldocuments");
            //await container.CreateAsync();
           
                // Get a reference to the blob just uploaded from the API in a container from configuration settings
            var blobClient = containerclient.GetBlobClient(FileName);

            using (var response = blobClient.UploadAsync(blob))
            {
                return response;
            }
 

        }
    }
}