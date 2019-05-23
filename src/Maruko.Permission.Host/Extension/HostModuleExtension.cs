using Autofac;
using Maruko.AutoMapper;
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
                new CoreModule(),               
                new HostModule {IsLastModule = true}
            });
        }
    }
}
