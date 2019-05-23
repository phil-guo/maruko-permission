using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Maruko.Application;
using Maruko.Application.Servers;
using Maruko.Application.Servers.Dto;
using Maruko.AutoMapper.AutoMapper;
using Maruko.Domain.Entities;
using Maruko.Domain.Repositories;
using Maruko.ObjectMapping;
using Maruko.Permission.Core.Utils;

namespace Maruko.Permission.Core.Application
{
    internal class CrudAppServiceCore<TEntity, TEntityDto, TSearch>
        : CurdAppService<TEntity, TEntityDto, TEntityDto, TEntityDto>,
            ICrudAppServiceCore<TEntity, TEntityDto, TSearch>
        where TEntity : class, IEntity<int>
        where TEntityDto : EntityDto<int>
        where TSearch : PageDto
    {
        public CrudAppServiceCore(IObjectMapper objectMapper, IRepository<TEntity> repository) : base(objectMapper,
            repository)
        {
        }

        public virtual ApiReponse<object> PageSearch(TSearch search)
        {
            var datas = Repository.PageSearch(out var total, SearchFilter(search), OrderFilter(),
                search.PageIndex, search.PageSize);
            return new ApiReponse<object>(ConvertToEntitieDtos(datas).DataToDictionary(total));
        }

        public ApiReponse<object> CreateOrEdit(TEntityDto model)
        {
            TEntity data = null;
            if (model.Id == 0)
            {
                data = Repository.Insert(MapToEntity(model));
            }
            else
            {
                data = Repository.SingleOrDefault(item => item.Id == model.Id);
                data = model.MapTo(data);
                data = Repository.Update(data);
            }

            return data == null
                ? new ApiReponse<object>("系统异常，操作失败", ServiceEnum.Failure)
                : new ApiReponse<object>("操作成功");
        }

        /// <summary>
        ///     查询条件赛选
        /// </summary>
        /// <returns></returns>
        protected virtual Expression<Func<TEntity, bool>> SearchFilter(TSearch search)
        {
            return null;
        }

        /// <summary>
        ///     排序条件筛选
        /// </summary>
        /// <returns></returns>
        protected virtual Expression<Func<TEntity, int>> OrderFilter()
        {
            return null;
        }
        
        /// <summary>
        ///     返回自定义数据对象
        ///     默认返回dto集合
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        protected virtual object ConvertToEntitieDtos(List<TEntity> entities)
        {
            return entities.MapTo<List<TEntityDto>>();
        }
    }
}