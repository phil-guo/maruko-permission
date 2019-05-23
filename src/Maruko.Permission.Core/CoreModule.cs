using System;

namespace Maruko.Permission.Core
{
    public class CoreModule : MarukoKernelModule
    {
        public override double Order { get; set; } = 1;
    }
}
