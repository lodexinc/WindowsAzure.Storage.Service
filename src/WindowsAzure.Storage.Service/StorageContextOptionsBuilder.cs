using Microsoft.WindowsAzure.Storage;

namespace PaterSantyago.WindowsAzure.Storage.Service
{
    public class StorageContextOptionsBuilder
    {
        public StorageContextOptionsBuilder()
            : this(new StorageContextOptions())
        {
            
        }

        public StorageContextOptionsBuilder(StorageContextOptions options)
        {
            Options = options;
        }

        public StorageContextOptions Options { get; private set; } 
        
        public StorageContextOptionsBuilder UseStorageAccount(string connectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            
            return UseStorageAccount(storageAccount);
        }

        public StorageContextOptionsBuilder UseStorageAccount(CloudStorageAccount storageAccount)
        {
            var options = Options ?? new StorageContextOptions();

            Options = options.WithStorageAccount(storageAccount);

            return this;
        }
    }
}