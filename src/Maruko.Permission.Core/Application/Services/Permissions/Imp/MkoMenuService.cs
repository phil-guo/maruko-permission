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
using System.Linq.Expressions;
using Maruko.Application;
using Maruko.AutoMapper.AutoMapper;
using Maruko.Domain.Specification;
using Maruko.ObjectMapping;
using Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoMenu;
using Maruko.Permission.Core.Domain.Permissions;
using Maruko.Permission.Core.Domain.Permissions.IRepos;
using Maruko.Permission.Core.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Maruko.Permission.Core.Application.Services.Permissions.Imp
{
    public class MkoMenuService : CrudAppServiceCore<MkoMenu, MkoMenuDto, SearchMkoMenuDto>, IMkoMenuService
    {
        public MkoMenuService(IObjectMapper objectMapper, IMkoMenuRepos repository) : base(objectMapper, repository)
        {
        }

        private readonly IMkoOperateRepos _operate;
        private readonly IMkoRoleMenuRepos _roleMenuRepos;

        public MkoMenuService(IObjectMapper objectMapper, IMkoMenuRepos repository, IMkoOperateRepos operate,
            IMkoRoleMenuRepos roleMenuRepos) : base(objectMapper, repository)
        {
            _operate = operate;
            _roleMenuRepos = roleMenuRepos;
        }

        /// <summary>
        ///     设置角色菜单权限时获取
        ///     没有首页的数据
        /// </summary>
        /// <returns></returns>
        public ApiReponse<object> GetRoleMenusNotHome(int roleId)
        {
            var result = new { menuIds = new List<string>(), list = new List<dynamic>() };

            var datas = Repository.GetAll().OrderBy(item => item.Id).AsNoTracking().ToList();
            var listMenus = new List<MenuByRoleDto>();
            datas.ForEach(item =>
            {
                listMenus.Add(new MenuByRoleDto
                {
                    ParentId = item.ParentId,
                    Id = item.Id,
                    Title = item.Name,
                    Icon = item.Icon,
                    Path = item.Url,
                    Operates = item.Operates,
                    Children = new List<MenuByRoleDto>()
                });
            });

            var tree = listMenus.Where(item => item.ParentId == 0).ToList();

            var operates = _operate
                .GetAll()
                .AsNoTracking()
                .Select(item => new
                {
                    item.Id,
                    item.Name,
                    item.Unique
                }).ToList();

            tree.ForEach(item => { BuildMeunsRecursiveTree(listMenus, item); });
            tree.ForEach(item =>
            {
                var model = new { Id = $"{item.Id}_0", Label = item.Title, Children = new List<dynamic>() };

                if (item.Children.Count > 0)
                    item.Children.ForEach(child =>
                    {
                        var operateModel = new
                        { Id = $"{child.Id}_0", Label = child.Title, Children = new List<dynamic>() };
                        model.Children.Add(operateModel);
                        operates.ForEach(op =>
                        {
                            operateModel.Children.Add(new { Id = $"{child.Id}_{op.Id}", Label = op.Name });
                        });
                    });
                result.list.Add(model);
            });
            var roleMenus = GetRoleOfMenus(roleId);
            roleMenus.ForEach(item =>
            {
                if (item.Children.Count > 0)
                    item.Children.ForEach(child =>
                    {
                        JsonConvert.DeserializeObject<List<int>>(child.Operates).ForEach(operateId =>
                        {
                            operates.ForEach(op =>
                            {
                                if (op.Id != operateId)
                                    return;
                                result.menuIds.Add($"{child.Id}_{op.Id}");
                            });
                        });
                    });
            });

            return new ApiReponse<object>(result, "查询成功");
        }

        public ApiReponse<object> GetMenusByRole(int roleId)
        {
            var tree = GetRoleOfMenus(roleId);
            var list = new List<dynamic> { new { Icon = "home", Title = "首页", Path = "/index" } };
            tree.ForEach(item =>
            {
                var model = new { item.Id, item.Title, item.Icon, Path = "", Children = new List<dynamic>() };

                if (item.Children.Count > 0)
                    item.Children.ForEach(child =>
                    {
                        model.Children.Add(new
                        { child.Id, child.Icon, Path = child.Path + "?id=" + child.Id, child.Title });
                    });

                list.Add(model);
            });

            return new ApiReponse<object>(list, "查询成功");
        }

        public override ApiReponse<object> CreateOrEdit(MkoMenuDto model)
        {
            var menu = model.MapTo<MkoMenu>();
            menu.Operates = JsonConvert.SerializeObject(model.OperateArray);

            if (menu.Id > 0)
            {
                var oldMenu = Repository.SingleOrDefault(item => item.Id == menu.Id);
                oldMenu.Operates = menu.Operates;
                oldMenu.ParentId = menu.ParentId;
                oldMenu.Name = menu.Name;
                oldMenu.Level = menu.Level;
                oldMenu.Url = menu.Url;
                oldMenu.Sort = menu.Sort;
                oldMenu.Icon = menu.Icon;
                menu = Repository.Update(oldMenu);
            }
            else
            {
                var lastMenu = Repository.GetAll().OrderByDescending(item => item.Id).LastOrDefault();
                if (lastMenu != null && model.Id == 0)
                    menu.Sort = lastMenu.AddOperateSort();
                menu = Repository.Insert(menu);
            }

            return menu == null
                ? new ApiReponse<object>("操作失败", ServiceEnum.Failure)
                : new ApiReponse<object>("添加或修改成功");
        }

        protected override Expression<Func<MkoMenu, bool>> SearchFilter(SearchMkoMenuDto search)
        {
            Expression<Func<MkoMenu, bool>> getFilter = item => true;
            if (search.ParentId.HasValue)
                getFilter = getFilter.And(item => item.ParentId == search.ParentId);
            return getFilter;
        }



        protected override object ConvertToEntitieDtos(List<MkoMenu> entities)
        {
            var datas = new List<SearchOutputDTO>();
            entities.ForEach(item =>
            {
                var data = new SearchOutputDTO
                {
                    Id = item.Id,
                    Level = item.Level,
                    Name = item.Name,
                    ParentId = item.ParentId,
                    Url = item.Url,
                    Operates = item.Operates,
                    Icon = item.Icon
                };
                JsonConvert.DeserializeObject<List<int>>(item.Operates).ForEach(operateId =>
                {
                    var model = _operate.SingleOrDefault(ope => ope.Id == operateId);
                    if (model == null)
                        return;
                    data.OperatesDictionary.Add(operateId, model.Name);
                });
                datas.Add(data);
            });
            return datas;
        }

        private List<MenuByRoleDto> GetRoleOfMenus(int roleId)
        {
            var datas = GetRoleMenu(roleId);

            var listMenus = new List<MenuByRoleDto>();
            datas.ForEach(item =>
            {
                listMenus.Add(new MenuByRoleDto
                {
                    ParentId = item.ParentId,
                    Id = item.Id,
                    Title = item.Name,
                    Icon = item.Icon,
                    Path = item.Url,
                    Operates = item.Operates
                });
            });

            var tree = listMenus.Where(item => item.ParentId == 0).ToList();

            tree.ForEach(item => { BuildMeunsRecursiveTree(listMenus, item); });

            return tree;
        }

        private List<MkoMenu> GetRoleMenu(int roleId)
        {
            var menus = new List<MkoMenu>();
            var roleMenusByRole = _roleMenuRepos.GetAll()
                .Where(item => item.RoleId == roleId)
                .OrderByDescending(item => item.MenuId)
                .ToList();

            if (roleMenusByRole.Count == 0)
                return menus;
            roleMenusByRole.ForEach(rm =>
            {
                var menu = FirstOrDefault(item => item.Id == rm.MenuId);
                menus.Add(menu);
            });
            return menus;
        }

        private void BuildMeunsRecursiveTree(List<MenuByRoleDto> list, MenuByRoleDto currentTree)
        {
            list.ForEach(item =>
            {
                if (item.ParentId == currentTree.Id) currentTree.Children.Add(item);
            });
        }
    }
}



