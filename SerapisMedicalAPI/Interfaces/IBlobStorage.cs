using Azure;
using Azure.Core;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SerapisMedicalAPI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace SerapisMedicalAPI.Interfaces
{
    public interface IBlobStorage
    {
        public Task<Stream> GetBlobAsync(string blobID, string containerID);

        public Task<IEnumerable<string>> ListBlobsAsync(string containerID);

        public Task DeleteBlobAsync(string blobID, string containerID);
        public Task<Response<BlobContentInfo>> UploadAsync(string containerID, string patientInfo);

        public Task<BlobProperties> GetBlobProperties(string blobID, string containerID);
    }

}
 