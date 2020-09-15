using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace EFCoreDBFirst.MyStore.DAL
{
    public class MyStoreDesignTimeService : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            var config = new ConfigurationBuilder()
                        .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                        .AddJsonFile("appsettings.json", false, true)
                        .Build();

            serviceCollection.AddSingleton(typeof(IConfiguration), config);
            serviceCollection.AddSingleton<INamedConnectionStringResolver, MyStoreNamedConnectionStringResolver>();
            serviceCollection.AddSingleton<ICSharpDbContextGenerator, MyStoreDbContextGenerator>();
            serviceCollection.AddSingleton<ICSharpEntityTypeGenerator, MyStoreEntityTypeGenerator>();
        }
    }
}
