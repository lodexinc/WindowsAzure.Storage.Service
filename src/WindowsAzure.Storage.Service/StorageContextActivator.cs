using System;
using Microsoft.Extensions.DependencyInjection;

namespace PaterSantyago.WindowsAzure.Storage.Service
{
    internal static class StorageContextActivator
    {
        [ThreadStatic]
        private static IServiceProvider _serviceProvider;

        public static IServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider;
            }
            set
            {
                _serviceProvider = value;
            }
        }

        public static TContext CreateInstance<TContext>(IServiceProvider serviceProvider)
            where TContext : StorageContextBase
        {
            try
            {
                _serviceProvider = serviceProvider;

                return (TContext)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TContext));
            }
            finally
            {
                _serviceProvider = null;
            }
        }

        public static TContext CreateInstance<TContext>(IServiceProvider serviceProvider, params object[] parameters)
            where TContext : StorageContextBase
        {
            try
            {
                _serviceProvider = serviceProvider;

                return (TContext)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TContext), parameters);
            }
            finally
            {
                _serviceProvider = null;
            }
        }
    }
}