using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EFCoreDBFirst.MyStore.DAL
{
    public class MyStoreNamedConnectionStringResolver : NamedConnectionStringResolverBase
    {
        private IServiceProvider _serviceProvider;
        protected override IServiceProvider ApplicationServiceProvider => _serviceProvider;

        public MyStoreNamedConnectionStringResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
