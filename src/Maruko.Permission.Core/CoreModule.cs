using System;
using Autofac;
using Maruko.Permission.Core.Utils.Cache;

namespace Maruko.Permission.Core
{
    public class CoreModule : MarukoKernelModule
    {
        public override double Order { get; set; } = 1;

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MarukoMemoryCahce>().As<IMarukoCache>().InstancePerDependency();
            base.Load(builder);
        }
    }
}
