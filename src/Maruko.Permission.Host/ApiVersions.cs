using System.ComponentModel.DataAnnotations;

namespace Maruko.Permission.Host
{
    /// <summary>
    /// 版本枚举
    /// </summary>
    public enum ApiVersions
    {
        [Display(Name = "V1", Description = "版本一")]
        V1,
        [Display(Name = "V2", Description = "版本二")]
        V2
    }
}
