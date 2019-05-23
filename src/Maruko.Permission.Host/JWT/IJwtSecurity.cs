using Maruko.Dependency;
using System.Collections.Generic;
using System.Security.Claims;

namespace Maruko.Permission.Host.JWT
{
    public interface IJwtSecurity : IDependencyTransient
    {
        /// <summary>
        /// 生成jwt
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        string JwtSecurityToken(int userId, string roleName);

        /// <summary>
        /// 生成jwt
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        string JwtSecurityToken(List<Claim> claims, string roleName);
    }
}
