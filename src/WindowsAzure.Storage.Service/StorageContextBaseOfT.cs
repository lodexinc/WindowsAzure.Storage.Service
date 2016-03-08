using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.WindowsAzure.Storage.Blob;

namespace PaterSantyago.WindowsAzure.Storage.Service
{
    public abstract class StorageContextBase<T> : StorageContextBase
        where T : StorageContextBase<T>
    {
        protected StorageContextBase()
        {
            Initialize();
        }

        protected StorageContextBase(StorageContextOptions options)
            : base(options)
        {
            Initialize();
        }

        private void Initialize()
        {
            BlobContainerBuilders = new Dictionary<PropertyInfo, BlobContainerBuilder>();

            var storageAccount = Options.StorageAccount;
            if (storageAccount == null)
            {
                throw new InvalidOperationException(
                    "StorageAccount is not configured");
            }

            var properties = GetType().GetProperties();

            var blobContainerProperties = properties.Where(x => x.PropertyType == typeof(CloudBlobContainer)).ToList();
            if (blobContainerProperties.Any())
            {
                var blobClient = storageAccount.CreateCloudBlobClient();

                foreach (var property in blobContainerProperties)
                {
                    var builder   = GetBlobContainerBuilder(property);
                    var container = builder.BuildBlobContainer(blobClient);

                    property.SetValue(this, container);
                }
            }
        }

        private Dictionary<PropertyInfo, BlobContainerBuilder> BlobContainerBuilders { get; set; }

        private BlobContainerBuilder GetBlobContainerBuilder(PropertyInfo property)
        {
            BlobContainerBuilder builder;

            return BlobContainerBuilders.TryGetValue(property, out builder)
                 ? builder
                 : new BlobContainerBuilder(property.Name);
        }

        protected virtual void OnContextCreating()
        {
            
        }

        protected BlobContainerBuilder UseBlobContainer(Expression<Func<T, CloudBlobContainer>> blobContainer)
        {
            var property = (PropertyInfo) ((MemberExpression) blobContainer.Body).Member;
            var builder  = new BlobContainerBuilder(blobContainer.Name);

            BlobContainerBuilders[property] = builder;

            return builder;
        }
    }
}