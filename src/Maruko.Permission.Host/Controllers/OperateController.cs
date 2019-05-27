using Maruko.Permission.Core.Application.Services.Permissions;
using Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoOperate;
using Maruko.Permission.Core.Domain.Permissions;
using Maruko.Permission.Core.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Maruko.Permission.Host.Controllers
{
    /// <summary>
    /// 功能/按钮
    /// </summary>
    [EnableCors("cors")]
    [ApiExplorerSettings(GroupName = "V1")]
    [Route("api/v1/operates/")]
    public class OperateController : ControllerBaseCrud<MkoOperate, MkoOperateDto, SearchMkoOperateDto>
    {
        private readonly IMkoOperateService _crud;
        public OperateController(IMkoOperateService crud) : base(crud)
        {
            _crud = crud;
        }

        /// <summary>
        /// 获取菜单下的所有功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("getMenuOfOperate")]
        public ApiReponse<object> GetMenuOfOperate(GetMenuOfOperateDto model)
        {
            return _crud.GetMenuOfOperate(model);
        }
    }
}
