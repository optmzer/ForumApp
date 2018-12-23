using ForumApp.Data;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
/**
 Manages image Uploads to the Azure Blob Storage
     */
namespace ForumApp.Services
{
    public class UploadService : IUpload
    {
        public CloudBlobContainer GetStorageContainer(string connectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();

            return blobClient.GetContainerReference("images");
        }
    }
}
