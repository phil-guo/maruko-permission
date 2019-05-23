using Maruko.Application.Servers;
using Maruko.Application.Servers.Dto;
using Maruko.Domain.Entities;
using Maruko.Permission.Core.Utils;

namespace Maruko.Permission.Core.Application
{
    public interface ICrudAppServiceCore<TEntity, TEntityDto, TSearch>
        : ICurdAppService<TEntity, TEntityDto, TEntityDto, TEntityDto>
        where TEntity : class, IEntity<int>
        where TEntityDto : EntityDto<int>
        where TSearch : PageDto
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        ApiReponse<object> PageSearch(TSearch search);

        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ApiReponse<object> CreateOrEdit(TEntityDto model);
    }
}
