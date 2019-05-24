using System.ComponentModel.DataAnnotations;

namespace Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoUser
{
    public class LoginInDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
    }
}
