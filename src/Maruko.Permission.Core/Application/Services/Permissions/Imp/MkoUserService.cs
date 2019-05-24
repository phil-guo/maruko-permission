//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：05-23-2019  
//版本1.0
//===================================================================================

using System;
using Maruko.Application;
using Maruko.ObjectMapping;
using Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoUser;
using Maruko.Permission.Core.Domain.Permissions;
using Maruko.Permission.Core.Domain.Permissions.IRepos;
using Maruko.Permission.Core.Utils;
using Maruko.Permission.Core.Utils.Cache;
using Maruko.Runtime.Security;

namespace Maruko.Permission.Core.Application.Services.Permissions.Imp
{
    public class MkoUserService : CrudAppServiceCore<MkoUser, MkoUserDto, SearchMkoUserDto>, IMkoUserService
    {
        private readonly IMarukoCache _cache;
        private readonly IMkoRoleRepos _role;

        public MkoUserService(IObjectMapper objectMapper, IMkoUserRepos repository, IMarukoCache cache,
            IMkoRoleRepos role) : base(objectMapper, repository)
        {
            _cache = cache;
            _role = role;
        }

        public override ApiReponse<object> CreateOrEdit(MkoUserDto model)
        {
            if (model.Id == 1)
                throw new MarukoException("超级管理员不允许被修改");

            MkoUser data = null;
            if (model.Id == 0)
            {
                model.Password = model.Password.Md5Encrypt();
                data = Repository.Insert(MapToEntity(model));
            }
            else
            {
                data = Repository.SingleOrDefault(item => item.Id == model.Id);
                if (data == null)
                    throw new MarukoException("系统异常，数据不存在");

                data.RoleId = model.RoleId;
                data.UserName = model.UserName;
                data = Repository.Update(data);
            }

            return data == null
                ? new ApiReponse<object>("系统异常，操作失败", ServiceEnum.Failure)
                : new ApiReponse<object>("操作成功");
        }

        public ApiReponse<object> LoginIn(LoginInDto model)
        {
            model.Password = model.Password.Md5Encrypt();

            var entity =
                Repository.SingleOrDefault(item => item.UserName == model.UserName && item.Password == model.Password);
            if (entity == null)
                throw new MarukoException("账号或者密码错误");

            //var token = _cache.Get<string>(AspNetMvcGlobal.TokenCacheKey(entity.Id.ToString()));
            //if (string.IsNullOrEmpty(token))
            //{
            //    token = _jwt.JwtSecurityToken(entity.Id, entity.RoleId.ToString());
            //    _cache.Set(AspNetMvcGlobal.TokenCacheKey(entity.Id.ToString()), token, TimeSpan.FromHours(2));
            //}

            return new ApiReponse<object>(new
            {
                entity.Id,
                entity.RoleId,
                entity.UserName,
                //entity.Amount,
                //Token = token,
            });
        }
    }
}