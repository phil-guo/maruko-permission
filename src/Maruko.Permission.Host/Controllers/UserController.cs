using System;
using Maruko.Permission.Core.Application.Services.Permissions;
using Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoUser;
using Maruko.Permission.Core.Domain.Permissions;
using Maruko.Permission.Core.Utils;
using Maruko.Permission.Core.Utils.Cache;
using Maruko.Permission.Host.JWT;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Maruko.Permission.Host.Controllers
{
    /// <summary>
    ///     系统用户
    /// </summary>
    [EnableCors("cors")]
    [ApiExplorerSettings(GroupName = "V1")]
    [Route("api/v1/users/")]
    public class UserController : ControllerBaseCrud<MkoUser, MkoUserDto, SearchMkoUserDto>
    {
        private readonly IMarukoCache _cache;
        private readonly IMkoUserService _crud;
        private readonly IJwtSecurity _jwt;

        public UserController(IMkoUserService crud, IMarukoCache cache, IJwtSecurity jwt) : base(crud)
        {
            _crud = crud;
            _cache = cache;
            _jwt = jwt;
        }

        /// <summary>
        ///     系统用户登陆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("loginIn")]
        public ApiReponse<object> LoginIn(LoginInDto model)
        {
            var data = _crud.LoginIn(model);

            var token = _cache.Get<string>($"token_{data.Id.ToString()}");
            if (string.IsNullOrEmpty(token))
            {
                token = _jwt.JwtSecurityToken(data.Id, data.RoleId.ToString());
                _cache.Set($"token_{data.Id.ToString()}",
                    token,
                    TimeSpan.FromHours(2));
            }

            data.Token = token;
            return new ApiReponse<object>(data);
        }
    }
}