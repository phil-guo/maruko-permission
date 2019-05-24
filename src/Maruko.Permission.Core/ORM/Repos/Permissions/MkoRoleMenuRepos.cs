
//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：05-23-2019  
//版本1.0
//===================================================================================

using System.Collections.Generic;
using System.Linq;
using Maruko.EntityFrameworkCore.UnitOfWork;
using Maruko.Extensions.Linq;
using Maruko.Permission.Core.Domain.Permissions;
using Maruko.Permission.Core.Domain.Permissions.IRepos;
using Microsoft.EntityFrameworkCore;

namespace Maruko.Permission.Core.ORM.Repos.Permissions
{
    public class MkoRoleMenuRepos : BaseRepository<MkoRoleMenu>, IMkoRoleMenuRepos
    {
        private readonly IMkoMenuRepos _menu;

        public MkoRoleMenuRepos(IEfUnitOfWork unitOfWork, IMkoMenuRepos menu) : base(unitOfWork)
        {
            _menu = menu;
        }

        public List<MkoMenu> GetMenusForRoleAsync(int roleId)
        {
            var sysMenus = new List<MkoMenu>();
            var roleMenusByRole = GetAll().AsNoTracking()
                .WhereIf(roleId != 0, item => item.RoleId == roleId)
                .OrderByDescending(item => item.MenuId).ToList();

            if (roleMenusByRole.Count == 0)
                return sysMenus;
            roleMenusByRole.ForEach(rm =>
            {
                var menu = _menu.FirstOrDefault(item => item.Id == rm.MenuId);
                menu.Operates = rm.Operates;
                sysMenus.Add(menu);
            });
            return sysMenus;
        }

        public List<MkoRoleMenu> GetSysRoleMenus(int? roleId)
        {
            var query = GetAll();
            if (roleId.HasValue)
                query = query
                    .Where(item => item.RoleId == roleId);
            var datas = query
                .AsNoTracking()
                .ToList();
            return datas;
        }
    }
}