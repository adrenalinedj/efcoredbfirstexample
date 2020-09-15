using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreDBFirst.MyStore
{
    public class MyStoreDesignTimeService : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ICSharpDbContextGenerator, MyStoreDbContextGenerator> ();
            serviceCollection.AddSingleton<ICSharpEntityTypeGenerator, MyStoreEntityTypeGenerator> ();
        }
    }
}
