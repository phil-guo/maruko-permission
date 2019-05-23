namespace Maruko.Permission.Host
{
    public class HostModule : MarukoKernelModule
    {
        public override double Order { get; set; } = 2;
    }
}
