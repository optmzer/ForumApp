using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumApp.Data
{
    public interface IUpload
    {
        CloudBlobContainer GetStorageContainer(string connectionString);



    }
}
