//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：05-23-2019  
//版本1.0
//===================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using Maruko.AutoMapper.AutoMapper;
using Maruko.ObjectMapping;
using Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoOperate;
using Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoRole;
using Maruko.Permission.Core.Domain.Permissions;
using Maruko.Permission.Core.Domain.Permissions.IRepos;
using Maruko.Permission.Core.Utils;
using Newtonsoft.Json;

namespace Maruko.Permission.Core.Application.Services.Permissions.Imp
{
    public class MkoRoleService : CrudAppServiceCore<MkoRole, MkoRoleDto, SearchMkoRoleDto>, IMkoRoleService
    {
        private readonly IMkoMenuRepos _menu;
        private readonly IMkoOperateRepos _operate;
        private readonly IMkoRoleMenuRepos _roleMenu;

        public MkoRoleService(IObjectMapper objectMapper, IMkoRoleRepos repository, IMkoRoleMenuRepos roleMenu,
            IMkoOperateRepos operate, IMkoMenuRepos menu) : base(objectMapper, repository)
        {
            _roleMenu = roleMenu;
            _operate = operate;
            _menu = menu;
        }

        public ApiReponse<object> SetRolePermission(SetRolePermissionDto input)
        {
            var datas = _roleMenu.GetAllList(item => item.RoleId == input.RoleId);
            if (datas.Count > 0)
                _roleMenu.Delete(item => item.RoleId == input.RoleId);

            if (input.MenuIds.Count == 0)
                return new ApiReponse<object>("设置成功");

            var models = new List<RolePermissionDto>();
            var list = new List<MkoRoleMenu>();

            RolePermissionDto model = null;
            input.MenuIds.ForEach(item =>
            {
                model = new RolePermissionDto();
                var operateArray = item.Split('_');
                if (Convert.ToInt32(operateArray.LastOrDefault()) == 0)
                {
                    if (models.FirstOrDefault(m => m.MenuId == Convert.ToInt32(operateArray.FirstOrDefault())) != null)
                        return;
                    model.MenuId = Convert.ToInt32(operateArray.FirstOrDefault());
                    models.Add(model);
                }
                else
                {
                    var data = models.FirstOrDefault(m => m.MenuId == Convert.ToInt32(operateArray.FirstOrDefault()));
                    if (data == null)
                    {
                        model.MenuId = Convert.ToInt32(operateArray.FirstOrDefault());
                        model.Operates.Add(Convert.ToInt32(operateArray.LastOrDefault()));
                        models.Add(model);
                    }
                    else
                        data.Operates.Add(Convert.ToInt32(operateArray.LastOrDefault()));
                }

            });

            models.ForEach(dy =>
            {
                var menu = _menu.SingleOrDefault(item => item.Id == dy.MenuId);
                if (menu == null)
                    return;

                var roleMenu = new MkoRoleMenu
                {
                    MenuId = dy.MenuId,
                    RoleId = input.RoleId,
                    Operates = JsonConvert.SerializeObject(menu.ParentId == 0
                        ? new List<int>()
                        : dy.Operates)
                };

                list.Add(roleMenu);
            });

            var result = _roleMenu.BatchInsert(list);
            if (!result)
                throw new MarukoException("设置失败");

            return new ApiReponse<object>("设置成功");
        }

        /// <summary>
        ///     获取角色的菜单
        /// </summary>
        /// <param name="roleId"></param>
        public ApiReponse<object> GetRoleOfMenus(int roleId)
        {
            var datas = _roleMenu.GetMenusForRoleAsync(roleId);

            var maps = datas.MapTo<List<RoleOfMenusOutput>>();

            var tree = maps.Where(item => item.ParentId == 0).ToList();

            tree.ForEach(item => { BuildMeunsRecursiveTree(maps, item); });

            return new ApiReponse<object>(tree, "查询成功");
        }

        #region 私有方法

        private void BuildMeunsRecursiveTree(List<RoleOfMenusOutput> list, RoleOfMenusOutput currentTree)
        {
            list.ForEach(item =>
            {
                if (item.ParentId == currentTree.Id)
                {
                    item.OperateArray = GetMenuForOperates(item.Operates);
                    currentTree.Nodes.Add(item);
                }
            });
        }

        private List<GetMenuOpeateOutput> GetMenuForOperates(string operateIds)
        {
            var operates = new List<GetMenuOpeateOutput>();
            var operateIdArray = JsonConvert.DeserializeObject<List<int>>(operateIds);
            operateIdArray.ForEach(item =>
            {
                var entity = _operate.FirstOrDefault(item);
                var operate = entity.MapTo<GetMenuOpeateOutput>();
                operates.Add(operate);
            });
            return operates;
        }

        #endregion
    }
}