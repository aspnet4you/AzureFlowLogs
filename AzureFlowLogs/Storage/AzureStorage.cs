using System;
using System.Collections.Generic;
using System.Linq;
using Aspnet4you.AzureFlowLogs;
using Aspnet4you.AzureFlowLogs.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Aspnet4you.AzureFlowLogs.Extensions;

namespace Aspnet4you.AzureFlowLogs.Storage
{
    public class AzureStorage
    {
        private CloudStorageAccount csa;
        private CloudBlobClient cloudBlobClient;
        private string flowLogContainer;

        public AzureStorage()
        {
            string azureStorageUri = Startup.Configuration["appSettings:" + GlobalConstants.AzureStorageUri];
            string azureStorageConnectionString = Startup.Configuration["appSettings:" + GlobalConstants.AzureStorageConnectionString];
            flowLogContainer = Startup.Configuration["appSettings:" + GlobalConstants.AzureStorageFlowLogsContainer];

            StorageUri storageUri = new StorageUri(new Uri(azureStorageUri));
            csa = CloudStorageAccount.Parse(azureStorageConnectionString);
        }

        
        internal CloudBlobContainer GetCloudBlobContainer(string containerName)
        {
            CloudBlobContainer cbContainer = cloudBlobClient.GetContainerReference(containerName);

            return cbContainer;
        }

        internal CloudBlobDirectory GetDirectory(CloudBlobContainer container, string relativePath)
        {
            CloudBlobDirectory cbDirectory = container.GetDirectoryReference(relativePath);

            return cbDirectory;
        }

        internal string[] GetListofBlobs(string path, int? maxResults)
        {
            List<string> list = new List<string>();

            cloudBlobClient = csa.CreateCloudBlobClient();
            CloudBlobContainer cbContainer = cloudBlobClient.GetContainerReference(flowLogContainer);
            CloudBlobDirectory cbDirectory = cbContainer.GetDirectoryReference(path);

            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var results = cbDirectory.ListBlobsSegmentedAsync(true, BlobListingDetails.Metadata, maxResults??25, blobContinuationToken,null,null).GetAwaiter().GetResult();
                // Get the value of the continuation token returned by the listing call.
                blobContinuationToken = results.ContinuationToken;
                foreach (IListBlobItem item in results.Results)
                {
                    list.Add((item as CloudBlockBlob).Name);
                }
            } while (blobContinuationToken != null); // Loop while the continuation token is not null.

            return list.ToArray();
        }

        internal NSGFlowLogs DownloadBlockBlob(string blobPath, string blobName)
        {
            cloudBlobClient = csa.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = GetCloudBlobContainer(flowLogContainer);
            CloudBlobDirectory cbDirectory = GetDirectory(cloudBlobContainer, blobPath);

            CloudBlockBlob cbBlob = cbDirectory.GetBlockBlobReference(blobName);
            string blockBlob = cbBlob.DownloadTextAsync().GetAwaiter().GetResult();
            NSGFlowLogs nsg = JsonConvert.DeserializeObject<NSGFlowLogs>(blockBlob);
            
            return nsg;
        }
    }
}
