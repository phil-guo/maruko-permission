using Maruko.Permission.Core.Domain.Permissions;
using Maruko.Runtime.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maruko.Permission.Core.ORM.Seed
{
    public class SeedSysUser : IEntityTypeConfiguration<MkoUser>
    {
        public void Configure(EntityTypeBuilder<MkoUser> builder)
        {
            builder.HasData(new MkoUser
            {
                Id = 1,
                UserName = "admin",
                Password = "123qwe".Md5Encrypt(),
                RoleId = 1
            });
        }
    }
}
