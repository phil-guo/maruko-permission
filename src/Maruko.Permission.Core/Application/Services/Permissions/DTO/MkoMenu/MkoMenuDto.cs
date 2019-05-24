//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：05-23-2019  
//版本1.0
//===================================================================================

using System.Collections.Generic;
using Maruko.Application.Servers.Dto;
using Maruko.AutoMapper.AutoMapper;

namespace Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoMenu
{
    [AutoMap(typeof(Domain.Permissions.MkoMenu))]
    public class MkoMenuDto : EntityDto
    {
        /// <summary>
        ///     父级
        /// </summary>
        public int ParentId { get; set; } = 0;

        /// <summary>
        ///     菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     菜单地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     层级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        ///     菜单权限(list<int /> json)
        /// </summary>
        public string Operates { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 菜单权限
        /// </summary>
        public List<int> OperateArray { get; set; } = new List<int>();
    }
}