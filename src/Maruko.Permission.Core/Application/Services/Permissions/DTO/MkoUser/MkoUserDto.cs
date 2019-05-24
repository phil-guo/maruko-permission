//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：05-23-2019  
//版本1.0
//===================================================================================

using Maruko.Application.Servers.Dto;
using Maruko.AutoMapper.AutoMapper;

namespace Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoUser
{
    [AutoMap(typeof(Domain.Permissions.MkoUser))]
    public class MkoUserDto : EntityDto
    {
        /// <summary>
        ///     角色id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        //[TryValidateNullOrEmptyModel(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }

        /// <summary>
        ///     密码
        /// </summary>
        public string Password { get; set; }
    }
}