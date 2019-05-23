using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Maruko.Permission.Core.ORM.Extension
{
    public static class EntityFrameworkExtension
    {
        public static IServiceCollection AddDeafultDbContext(this IServiceCollection serviceCollection)
        {
            var connStr = ConfigurationSetting.DefaultConfiguration.GetConnectionString("DefaultConnection");
            serviceCollection
                .AddDbContextPool<DefaultDbContext>(options => { options.UseMySql(connStr); });
            return serviceCollection;
        }
    }
}
