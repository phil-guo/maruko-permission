using Maruko.Application.Servers.Dto;
using Maruko.Domain.Entities;
using Maruko.Permission.Core.Application;
using Maruko.Permission.Core.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Maruko.Permission.Host
{
    /// <summary>
    /// 控制器crud操作基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TSearch"></typeparam>
    [ApiController]
    public class ControllerBaseCrud<TEntity, TEntityDto, TSearch> : ControllerBase
        where TEntity : class, IEntity<int>
        where TEntityDto : EntityDto
        where TSearch : PageDto
    {
        private readonly ICrudAppServiceCore<TEntity, TEntityDto, TSearch> _crud;

        protected ControllerBaseCrud(ICrudAppServiceCore<TEntity, TEntityDto, TSearch> crud)
        {
            _crud = crud;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("pageSearch")]
        //[Authorize(Roles = "1,3")]//权限角色id
        public virtual ApiReponse<object> PageSearch(TSearch search)
        {
            return _crud.PageSearch(search);
        }

        /// <summary>
        /// 创建或修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("createOrEdit")]
        //[Authorize(Roles = "1,3")]//权限角色id
        public virtual ApiReponse<object> CreateOrUpdate(TEntityDto model)
        {
            return _crud.CreateOrEdit(model);
        }
    }
}
