using System;
using System.Linq;
using log4net;
using Maruko.Domain.Entities.Auditing;
using Maruko.Domain.UnitOfWork;
using Maruko.EntityFrameworkCore.UnitOfWork;
using Maruko.Logger;
using Microsoft.EntityFrameworkCore;

namespace Maruko.Permission.Core.ORM
{
    public class EfCoreBaseUow : IEfUnitOfWork
    {
        private readonly DefaultDbContext _defaultDbContext;
        public ILog Logger { get; }

        public EfCoreBaseUow(DefaultDbContext defaultDbContext)
        {
            _defaultDbContext = defaultDbContext;
            Logger = LogHelper.Log4NetInstance.LogFactory(typeof(EfCoreBaseUow));
        }

        public void Dispose()
        {
            _defaultDbContext.Dispose();
        }

        public int Commit()
        {
            try
            {
                //这里如果没有调用过createset方法就会报错，如果没有调用认为没有任何改变直接跳出来
                if (_defaultDbContext == null)
                    return 0;
                return _defaultDbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Logger.Debug(nameof(DbUpdateException) + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed;
            do
            {
                try
                {
                    if (_defaultDbContext == null)
                        return;

                    _defaultDbContext.SaveChanges();
                    saveFailed = false;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;
                    ex.Entries.ToList()
                        .ForEach(entry => entry.OriginalValues.SetValues(entry.GetDatabaseValues()));
                }
                catch (DbUpdateException ex)
                {
                    Logger.Debug(nameof(DbUpdateException) + ex.Message);
                    throw new Exception(ex.Message);
                }
            } while (saveFailed);
        }

        public void RollbackChanges()
        {
            if (_defaultDbContext != null)
                _defaultDbContext.ChangeTracker.Entries()
                    .ToList()
                    .ForEach(entry =>
                    {
                        if (entry.State != EntityState.Unchanged)
                            entry.State = EntityState.Detached;
                    });
        }

        public int ExecuteCommand(string sqlCommand, ContextType contextType = ContextType.DefaultContextType, params object[] parameters)
        {
            return _defaultDbContext.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        public DbSet<TEntity> CreateSet<TEntity, TPrimaryKey>() where TEntity : FullAuditedEntity<TPrimaryKey>
        {
            return _defaultDbContext.CreateSet<TEntity, TPrimaryKey>();
        }

        public void SetModify<TEntity, TPrimaryKey>(TEntity entity) where TEntity : FullAuditedEntity<TPrimaryKey>
        {
            _defaultDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
