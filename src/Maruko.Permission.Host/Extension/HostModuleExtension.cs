using Autofac;
using Maruko.AutoMapper;
using Maruko.EntityFrameworkCore;
using Maruko.Extensions;
using Maruko.Modules;
using Maruko.Permission.Core;

namespace Maruko.Permission.Host.Extension
{
    public static class HostModuleExtension
    {
        public static void RegisterModules(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModulesExtension(() => new MarukoModule[]
            {
                new MarukoKernelModule(),
                new AutoMapperModule(),
                new EntityFrameworkCoreModule(),
                new CoreModule(),               
                new HostModule {IsLastModule = true}
            });
        }
    }
}
