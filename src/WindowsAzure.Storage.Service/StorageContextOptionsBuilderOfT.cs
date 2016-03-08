using Microsoft.WindowsAzure.Storage;

namespace PaterSantyago.WindowsAzure.Storage.Service
{
    public class StorageContextOptionsBuilder<T> : StorageContextOptionsBuilder
        where T : StorageContextBase
    {
        public StorageContextOptionsBuilder()
            : this(new StorageContextOptions<T>())
        {
            
        }

        public StorageContextOptionsBuilder(StorageContextOptions options)
            : base(options)
        {
            
        }

        public new virtual StorageContextOptions<T> Options
            => (StorageContextOptions<T>) base.Options;

        public new virtual StorageContextOptionsBuilder<T> UseStorageAccount(string connectionString)
            => (StorageContextOptionsBuilder<T>) base.UseStorageAccount(connectionString);

        public new virtual StorageContextOptionsBuilder<T> UseStorageAccount(CloudStorageAccount storageAccount)
            => (StorageContextOptionsBuilder<T>) base.UseStorageAccount(storageAccount);
    }
}