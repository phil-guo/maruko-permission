using Maruko.EntityFrameworkCore.Context;
using Maruko.Permission.Core.Domain.Permissions;
using Microsoft.EntityFrameworkCore;

namespace Maruko.Permission.Core.ORM
{
    /// <summary>
    /// 提供一个默认的efcore上下文
    /// </summary>
    public class DefaultDbContext : BaseDbContext
    {

        //添加上下文
        public DbSet<MkoUser> MkoUsers { get; set; }
        public DbSet<MkoOperate> MkoOperates { get; set; }
        public DbSet<MkoMenu> MkoMenus { get; set; }
        public DbSet<MkoRole> MkoRoles { get; set; }
        public DbSet<MkoRoleMenu> MkoRoleMenus { get; set; }

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
