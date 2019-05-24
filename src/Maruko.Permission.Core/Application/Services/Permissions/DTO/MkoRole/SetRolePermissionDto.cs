using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoMenu;

namespace Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoRole
{
    public class SetRolePermissionDto
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public int RoleId { get; set; }

        public List<string> MenuIds { get; set; }
        ///// <summary>
        ///// 角色下的菜单集合
        ///// </summary>
        //public List<SetRolePermissionInput> MenuRoles { get; set; } = new List<SetRolePermissionInput>();
    }

    public class SetRolePermissionInput
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 角色下的菜单集合
        /// </summary>
        public SetMenuPermissionDto Menu { get; set; } = new SetMenuPermissionDto();
    }
}
