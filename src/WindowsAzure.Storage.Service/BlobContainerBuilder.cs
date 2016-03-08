using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace PaterSantyago.WindowsAzure.Storage.Service
{
    public class BlobContainerBuilder
    {
        public BlobContainerBuilder(string propertyName)
        {
            ContainerName         = propertyName.ToLowerInvariant();
            NeedCreateIfNotExists = false;
            OperationContext      = null;
            PublicAccessType      = BlobContainerPublicAccessType.Off;
            RequestOptions        = null;
        }
        
        private string ContainerName { get; set; }
        private bool NeedCreateIfNotExists { get; set; }
        private OperationContext OperationContext { get; set; }
        private BlobContainerPublicAccessType PublicAccessType { get; set; }
        private BlobRequestOptions RequestOptions { get; set; }

        internal CloudBlobContainer BuildBlobContainer(CloudBlobClient blobClient)
        {
            var container = blobClient.GetContainerReference(ContainerName);

            if (NeedCreateIfNotExists)
            {
                container.CreateIfNotExists(PublicAccessType, RequestOptions, OperationContext);
            }

            return container;
        }

        public BlobContainerBuilder CreateIfNotExists()
        {
            NeedCreateIfNotExists = true;

            return this;
        }

        public BlobContainerBuilder WithContainerName(string containerName)
        {
            ContainerName = containerName;

            return this;
        }

        public BlobContainerBuilder WithOperationContext(OperationContext operationContext)
        {
            OperationContext = operationContext;

            return this;
        }

        public BlobContainerBuilder WithPublicAccessType(BlobContainerPublicAccessType publicAccessType)
        {
            PublicAccessType = publicAccessType;

            return this;
        }

        public BlobContainerBuilder WithRequestOptions(BlobRequestOptions requestOptions)
        {
            RequestOptions = requestOptions;

            return this;
        }
    }
}