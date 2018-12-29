using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;
using WebApp.Core.Interfaces;

namespace WebApp.Infrastructure.BlobStorage
{
    public class BlobStorage : IBlobStorage
    {

        private CloudBlobContainer container = null;


        public BlobStorage(string connectionString)
        {
            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudBlobClient();
            container = client.GetContainerReference("kopoolen");
            container.CreateIfNotExistsAsync();
            container.SetPermissionsAsync(
                new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob }
            );
        }

        public async Task<string> SaveFile(IFormFile file)
        {

            if (file == null)
            {
                throw new System.ArgumentNullException(nameof(file));
            }

            var id = Guid.NewGuid().ToString();

            var filename = String.Format("{0}-{1}{2}",
                Path.GetFileNameWithoutExtension(file.FileName),
                id,
                Path.GetExtension(file.FileName)
            );

            using (MemoryStream stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Seek(0, SeekOrigin.Begin);
                var blob = container.GetBlockBlobReference(blobName: filename);
                await blob.UploadFromStreamAsync(stream);
                return blob.Uri.AbsolutePath;
            }

        }

    }
}
