using Maruko.Permission.Core.Application.Services.Permissions;
using Maruko.Permission.Core.Application.Services.Permissions.DTO.SysRole;
using Maruko.Permission.Core.Domain.Permissions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Maruko.Permission.Host.Controllers
{
    /// <summary>
    /// 角色
    /// </summary>
    [EnableCors("cors")]
    [ApiExplorerSettings(GroupName = "V1")]
    [Route("api/v1/roles/")]

    public class RoleController : ControllerBaseCrud<MkoRole, MkoRoleDto, SearchMkoRoleDto>
    {
        private readonly IMkoRoleService _crud;
        public RoleController(IMkoRoleService crud) : base(crud)
        {
            _crud = crud;
        }

        ///// <summary>
        ///// 设置角色权限
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost("setRolePermission")]
        //public ApiReponse<object> SetRolePermission(SetRolePermissionDto model)
        //{
        //    return _crud.SetRolePermission(model);
        //}

        ///// <summary>
        ///// 获取角色菜单
        ///// roleId=0时获取全部菜单
        ///// </summary>
        ///// <param name="roleId"></param>
        ///// <returns></returns>
        //[HttpGet("getRoleOfMenus")]
        //public ApiReponse<object> GetRoleOfMenus(int roleId)
        //{
        //    return _crud.GetRoleOfMenus(roleId);
        //}
    }
}
