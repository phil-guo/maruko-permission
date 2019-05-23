using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Maruko.Domain.Entities.Auditing;

namespace Maruko.Permission.Core.Domain.Permissions
{
    [Table("sys_rolemenu")]
    public class MkoRoleMenu : FullAuditedEntity<int>
    {
        /// <summary>
        ///     角色id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        ///     菜单id
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        ///     操作权限，(list<object/> json)
        /// </summary>
        [StringLength(200)]
        public string Operates { get; set; }
    }
}
