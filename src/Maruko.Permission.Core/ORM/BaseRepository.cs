using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Maruko.Domain.Entities.Auditing;
using Maruko.Domain.Repositories;
using Maruko.EntityFrameworkCore.Repository;
using Maruko.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Maruko.Permission.Core.ORM
{
    public class BaseRepository<TEntity> : MarukoBaseRepository<TEntity, int>,
        IRepository<TEntity>
        where TEntity : FullAuditedEntity<int>
    {
        public BaseRepository(IEfUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="total"></param>
        /// <param name="predicate"></param>
        /// <param name="orderSelector"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageMax"></param>
        /// <returns></returns>
        public List<TEntity> PageSearch(out int total, Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, int>> orderSelector = null, int pageIndex = 1,
            int pageMax = 20)
        {
            var query = GetAll().AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);

            query = orderSelector != null
                ? query.OrderByDescending(orderSelector)
                : query.OrderByDescending(item => item.Id);

            total = query.Count();

            var result = query
                .Skip(pageMax * (pageIndex - 1))
                .Take(pageMax)
                .ToList();
            return result;
        }


        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public bool BatchInsert(List<TEntity> entities)
        {
            entities.ForEach(item => { GetSet().Add(item); });
            var result = UnitOfWork.Commit();
            return result != 0;
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public bool BatchUpdate(List<TEntity> entities)
        {
            entities.ForEach(item => { _unitOfWork.SetModify<TEntity, int>(item); });
            var result = UnitOfWork.Commit();
            return result != 0;
        }
    }
}
