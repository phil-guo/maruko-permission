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
using Maruko.Application;
using Maruko.ObjectMapping;
using Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoOperate;
using Maruko.Permission.Core.Domain.Permissions;
using Maruko.Permission.Core.Domain.Permissions.IRepos;
using Maruko.Permission.Core.Utils;
using Newtonsoft.Json;

namespace Maruko.Permission.Core.Application.Services.Permissions.Imp
{
    public class MkoOperateService : CrudAppServiceCore<MkoOperate, MkoOperateDto, SearchMkoOperateDto>,
        IMkoOperateService
    {
        private readonly IMkoRoleMenuRepos _roleMenu;

        public MkoOperateService(IObjectMapper objectMapper, IMkoOperateRepos repository, IMkoRoleMenuRepos roleMenu) :
            base(objectMapper, repository)
        {
            _roleMenu = roleMenu;
        }

        public override ApiReponse<object> CreateOrEdit(MkoOperateDto model)
        {
            MkoOperate data = null;
            if (model.Id == 0)
            {
                var entity = Repository
                    .GetAll()
                    .OrderBy(item => item.Id)
                    .LastOrDefault();
                if (entity != null)
                {
                    model.Remark += (Convert.ToInt32(entity.Remark) + 1).ToString();
                    model.Unique += entity.Unique;
                }
                else
                {
                    model.Remark = "1001";
                    model.Unique = 10001;
                }

                data = Repository.Insert(MapToEntity(model));
            }
            else
            {
                data = Repository.SingleOrDefault(item => item.Id == model.Id);
                data.Name = model.Name;
                data = Repository.Update(data);
            }

            return data == null
                ? new ApiReponse<object>("系统异常，操作失败", ServiceEnum.Failure)
                : new ApiReponse<object>("操作成功");
        }

        public ApiReponse<object> GetMenuOfOperate(GetMenuOfOperateDto model)
        {
            var roleMenu =
                _roleMenu.SingleOrDefault(item => item.RoleId == model.RoleId && item.MenuId == model.MenuId);
            var idNos = new List<string>();
            JsonConvert.DeserializeObject<List<int>>(roleMenu?.Operates).ForEach(id =>
            {
                var operate = Repository.SingleOrDefault(item => item.Id == id);
                idNos.Add(operate?.Remark);
            });

            return new ApiReponse<object>(idNos, "查询成功");
        }
    }
}