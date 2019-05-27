using Maruko.Permission.Core.Application.Services.Permissions;
using Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoMenu;
using Maruko.Permission.Core.Domain.Permissions;
using Maruko.Permission.Core.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Maruko.Permission.Host.Controllers
{
    [EnableCors("cors")]
    [ApiExplorerSettings(GroupName = "V1")]
    [Route("api/v1/menus/")]
    public class MenuController : ControllerBaseCrud<MkoMenu, MkoMenuDto, SearchMkoMenuDto>
    {
        private readonly IMkoMenuService _crud;
        public MenuController(IMkoMenuService crud) : base(crud)
        {
            _crud = crud;
        }

        /// <summary>
        /// 设置角色菜单权限时获取
        /// 没有首页的数据
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("getRoleMenusNotHome")]
        public ApiReponse<object> GetRoleMenusNotHome(int roleId)
        {
            return _crud.GetRoleMenusNotHome(roleId);
        }

        /// <summary>
        /// 获取角色的菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("getMenusByRole")]
        public ApiReponse<object> GetRoleByMenus(int roleId)
        {
            return _crud.GetMenusByRole(roleId);
        }
    }
}
