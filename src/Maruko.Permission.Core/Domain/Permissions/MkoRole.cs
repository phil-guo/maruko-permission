using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Maruko.Domain.Entities.Auditing;

namespace Maruko.Permission.Core.Domain.Permissions
{
    /// <summary>
    ///     角色
    /// </summary>
    [Table("sys_role")]
    public class MkoRole : FullAuditedEntity<int>
    {
        /// <summary>
        ///     角色名称
        /// </summary>
        [StringLength(20)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        [StringLength(int.MaxValue)]
        public string Remark { get; set; }
    }
}
