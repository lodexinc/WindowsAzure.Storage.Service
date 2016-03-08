using Microsoft.WindowsAzure.Storage;

namespace PaterSantyago.WindowsAzure.Storage.Service
{
    public class StorageContextOptions
    {
        public StorageContextOptions()
        {

        }

        private StorageContextOptions(StorageContextOptions options)
        {
            StorageAccount = options.StorageAccount;
        }

        public CloudStorageAccount StorageAccount { get; private set; }

        public StorageContextOptions WithStorageAccount(CloudStorageAccount storageAccount)
        {
            return new StorageContextOptions(this)
            {
                StorageAccount = storageAccount
            };
        }
    }
}