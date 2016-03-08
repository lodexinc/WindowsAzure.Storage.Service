using System;
using Microsoft.Extensions.DependencyInjection;

namespace PaterSantyago.WindowsAzure.Storage.Service
{
    public class StorageServicesBuilder
    {
        private readonly IServiceCollection _serviceCollection;
        
        public StorageServicesBuilder(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public StorageServicesBuilder AddStorageContext<TContext>(Action<StorageContextOptionsBuilder> optionsAction = null)
            where TContext : StorageContextBase
        {
            _serviceCollection.AddSingleton(
                s => StorageContextOptionsFactory<TContext>(optionsAction));

            _serviceCollection.AddScoped(
                typeof(TContext), StorageContextActivator.CreateInstance<TContext>);

            return this;
        }

        private static StorageContextOptions<TContext> StorageContextOptionsFactory<TContext>(Action<StorageContextOptionsBuilder> optionsAction)
            where TContext : StorageContextBase
        {
            var options = new StorageContextOptions<TContext>();

            if (optionsAction != null)
            {
                var builder = new StorageContextOptionsBuilder<TContext>(options);

                optionsAction(builder);

                options = builder.Options;
            }

            return options;
        }
    }
}