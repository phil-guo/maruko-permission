using Maruko.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Maruko.Permission.Core.ORM
{
    /// <summary>
    /// 提供一个默认的efcore上下文
    /// </summary>
    public class DefaultDbContext : BaseDbContext
    {

        //添加上下文

        public DefaultDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //初始化数据

            //创建索引
        }
    }
}
