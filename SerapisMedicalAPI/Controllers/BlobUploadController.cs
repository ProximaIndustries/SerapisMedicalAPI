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

        [HttpGet("{blobName}")]
        public async Task<Stream> GetBlobAsync(string blobName)
        {
            //function to be defined in interface
            var data = await _uploadService.GetBlobAsync(blobName);
            return data;
        }



        [HttpGet(template:"list")]
        public async Task<IActionResult> ListBlobsAsync()
        {
            //function to be defined in interface
            return Ok(await _uploadService.ListBlobsAsync());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route (template: "upload")]

        public async Task UploadAsync(string FileName)
        {
            using var reader = new StreamReader(HttpContext.Request.Body);

            string body = await reader.ReadToEndAsync();

            var response = await _uploadService.UploadAsync(body, FileName);
            //return body;

        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteFile(string blobName)
        {
            //function to be defined in interface
            await _uploadService.DeleteBlobAsync(blobName);
            return Ok();
        }
       
    }
}
