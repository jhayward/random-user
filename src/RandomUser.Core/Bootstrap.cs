using System;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using RandomUser.Core.Infrastructure;

namespace RandomUser.Core
{
    public static class Bootstrap
    {
        /// <summary>
        /// Setup Infrastructure Services
        /// </summary>
        public static void AddCoreInfrastructure(this IServiceCollection services, string dbConnectionString)
        {
            if (dbConnectionString == null)
                throw new ArgumentNullException(nameof(dbConnectionString));

            services.AddSingleton(new LiteDatabase(dbConnectionString));
            services.AddSingleton<IUserStore, LiteDbUserStore>();
            services.AddSingleton<ISeedDataStore>(new StaticSeedDataStore());
        }
    }
}
