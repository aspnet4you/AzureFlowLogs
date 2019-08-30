using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aspnet4you.AzureFlowLogs.Models;
using Aspnet4you.AzureFlowLogs.Storage;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Aspnet4you.AzureFlowLogs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureStorageController : ControllerBase
    {
        private AzureStorage azureStorage;
        public AzureStorageController()
        {
            azureStorage = new AzureStorage();
        }

        [HttpGet("GetListofBlobs")]
        public string[] GetListofBlobs(string path, int? maxResults)
        {
            string[] blobList = azureStorage.GetListofBlobs(path, maxResults);
            return blobList;
        }

        [HttpGet("DownloadBlockBlob")]
        public NSGFlowLogs DownloadBlockBlob(string blobPath, string blobName)
        {
            NSGFlowLogs blockBlob = azureStorage.DownloadBlockBlob(blobPath, blobName);
            return blockBlob;
        }
    }
}
