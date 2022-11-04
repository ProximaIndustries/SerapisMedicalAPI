using Microsoft.AspNetCore.Mvc;
using SerapisMedicalAPI.Interfaces;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Postgrest.Responses;
using Azure;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.Collections;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Websocket.Client;
using SerapisMedicalAPI.Model;
using Azure.Storage.Blobs.Models;
using System;


namespace SerapisMedicalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobUploadController : ControllerBase
    {
        private readonly IBlobStorage _uploadService;

        public BlobUploadController(IBlobStorage uploadService)
        {
            _uploadService = uploadService; 
        }

        [HttpGet("blob")]
        public async Task<Stream> GetBlobAsync([FromQuery] string blobID, [FromQuery] string containerID)
        {
            //function to be defined in interface
            var data = await _uploadService.GetBlobAsync(blobID,containerID);
            return data;
        }

        [HttpGet("blobProperties")]
        public async Task<String> GetBlobProperties([FromQuery] string blobID, [FromQuery] string containerID)
        {
            //function to be defined in interface
            var data = await _uploadService.GetBlobProperties(blobID, containerID);

            var contentType = data.ContentType;
            return contentType;
        }

        [HttpGet(template: "list")]
        public async Task<IActionResult> ListBlobsAsync([FromQuery]string containerID)
        {

           
            //function to be defined in interface
            return Ok(await _uploadService.ListBlobsAsync(containerID));
        }

        [HttpPost]
        [Route (template: "upload")]

        public async Task UploadAsync([FromQuery] string containerID)
        {
            using var reader = new StreamReader(HttpContext.Request.Body);


            string body = await reader.ReadToEndAsync();

            await _uploadService.UploadAsync(containerID, body);

        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteFile([FromQuery] string blobID, [FromQuery] string containerID)
        {
            //function to be defined in interface
            await _uploadService.DeleteBlobAsync(blobID,containerID);
            return Ok();
        }
       
    }
}
