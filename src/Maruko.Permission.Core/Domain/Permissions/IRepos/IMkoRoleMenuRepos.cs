
//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：05-23-2019  
//版本1.0
//===================================================================================

using System.Collections.Generic;
using Maruko.Dependency;
using Maruko.Domain.Repositories;

namespace Maruko.Permission.Core.Domain.Permissions.IRepos
{
    public interface IMkoRoleMenuRepos : IRepository<MkoRoleMenu>, IDependencyScoped
    {
        /// <summary>
        /// 获取角色的菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<MkoMenu> GetMenusForRoleAsync(int roleId);


        List<MkoRoleMenu> GetSysRoleMenus(int? roleId);
    }
}
