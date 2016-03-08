using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

namespace PaterSantyago.WindowsAzure.Storage.Service
{
    public abstract class StorageContextBase
    {
        private static readonly ConcurrentDictionary<Type, Type> OptionsTypes;

        static StorageContextBase()
        {
            OptionsTypes = new ConcurrentDictionary<Type, Type>();
        }

        protected StorageContextBase()
        {
            Options = GetOptions(StorageContextActivator.ServiceProvider);
        }
        
        protected StorageContextBase(StorageContextOptions options)
        {
            Options = options;
        }
        
        protected StorageContextOptions Options { get; }
        
        private StorageContextOptions GetOptions(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var optionsType = OptionsTypes.GetOrAdd(GetType(), t => typeof(StorageContextOptions<>).MakeGenericType(t));
            var options     = serviceProvider.GetService(optionsType) 
                           ?? serviceProvider.GetService<StorageContextOptions>();

            if (options == null)
            {
                throw new InvalidOperationException(
                    $"Neither {optionsType.Name}, nor {nameof(StorageContextOptions)} object is registered");
            }

            return (StorageContextOptions) options;
        }
    }
}