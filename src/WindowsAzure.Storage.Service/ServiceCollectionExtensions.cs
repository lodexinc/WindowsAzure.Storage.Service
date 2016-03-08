using System;
using Microsoft.Extensions.DependencyInjection;

namespace PaterSantyago.WindowsAzure.Storage.Service
{
    public static class ServiceCollectionExtensions
    {
        public static StorageServicesBuilder AddAzureStorage(this IServiceCollection serviceCollection)
        {
            return new StorageServicesBuilder(serviceCollection);
        }
    }
}