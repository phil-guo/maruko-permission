using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Maruko.Domain.Entities.Auditing;

namespace Maruko.Permission.Core.Domain.Permissions
{
    [Table("sys_operate")]
    public class MkoOperate : FullAuditedEntity<int>
    {
        /// <summary>
        ///     按钮名称
        /// </summary>
        [StringLength(20)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        [StringLength(int.MaxValue)]
        public string Remark { get; set; }

        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required]
        public int Unique { get; set; }
    }
}
