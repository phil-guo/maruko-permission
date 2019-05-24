using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoMenu
{
    public class SetMenuPermissionDto
    {
        /// <summary>
        /// 菜单id
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 功能集合
        /// </summary>
        public List<int> Operates { get; set; }
    }
}
