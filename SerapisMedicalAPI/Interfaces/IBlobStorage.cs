using Azure;
using Azure.Core;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SerapisMedicalAPI.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace SerapisMedicalAPI.Interfaces
{
    public interface IBlobStorage
    {
        public Task<Stream> GetBlobAsync(string blobName);

        public Task<IEnumerable<string>> ListBlobsAsync();

        public Task DeleteBlobAsync(string blobName);
        public Task<Task<Response<BlobContentInfo>>> UploadAsync(string blob, string FileName);
    }

}
 