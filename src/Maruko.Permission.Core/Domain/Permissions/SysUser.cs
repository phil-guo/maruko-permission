using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Maruko.Domain.Entities.Auditing;

namespace Maruko.Permission.Core.Domain.Permissions
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [Table("sys_user")]
    public class MkoUser : FullAuditedEntity<int>
    {
        /// <summary>
        ///     角色id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [StringLength(32)]
        [Required]
        public string UserName { get; set; }

        /// <summary>
        ///     密码
        /// </summary>
        [StringLength(500)]
        [Required]
        public string Password { get; set; }
    }
}
