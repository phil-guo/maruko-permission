using Maruko.Permission.Core.Application.Services.Permissions;
using Maruko.Permission.Core.Application.Services.Permissions.DTO.SysUser;
using Maruko.Permission.Core.Domain.Permissions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Maruko.Permission.Host.Controllers
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [EnableCors("cors")]
    [ApiExplorerSettings(GroupName = "V1")]
    [Route("api/v1/users/")]
    public class UserController : ControllerBaseCrud<MkoUser, MkoUserDto, SearchMkoUserDto>
    {
        private readonly IMkoUserService _crud;
        public UserController(IMkoUserService crud) : base(crud)
        {
            _crud = crud;
        }
        
        ///// <summary>
        ///// 系统用户登陆
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost("loginIn")]
        //public ApiReponse<object> LoginIn(LoginInSysDto model)
        //{
        //    return _crud.LoginIn(model);
        //}
    }
}
