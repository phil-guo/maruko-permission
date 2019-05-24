using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoOperate;

namespace Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoRole
{
    public class RoleOfMenusOutput
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 父级
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        public string Url { get; set; }


        public string Operates { get; set; }

        /// <summary>
        /// 子集合
        /// </summary>
        public List<RoleOfMenusOutput> Nodes { get; set; } = new List<RoleOfMenusOutput>();


        /// <summary>
        /// 菜单下的按钮操作集合
        /// </summary>
        public List<GetMenuOpeateOutput> OperateArray { get; set; } = new List<GetMenuOpeateOutput>();
    }
}
