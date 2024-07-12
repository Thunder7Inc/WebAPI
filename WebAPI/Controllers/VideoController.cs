using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _blobContainerName = "thunder";

        public VideoController(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        [HttpGet("stream")]
        public async Task<IActionResult> StreamVideo()
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
            var blobClient = blobContainerClient.GetBlobClient("atm_project_output.mp4");

            var content = await blobClient.DownloadStreamingAsync();

            var response = File(content.Value.Content, "video/mp4");
            response.EnableRangeProcessing = true;
            return response;
        }
    }
}