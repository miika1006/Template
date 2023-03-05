using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Template.Core.Base;
using Template.Infrastructure.Data.Repositories.Base;

namespace Template.Infrastructure.Data
{
	public static class Database
	{
        /// <summary>
        /// Create database or migrate to latest version 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="connectionString"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static async Task CreateOrUpdateDatabaseAsync(this IApplicationBuilder app, string connectionString, ILogger logger)
        {
            await using var dbContext = new ApplicationDbContext(connectionString);
            if (dbContext.Database.IsRelational())
            {
                logger.LogInformation("Creating or updating database", connectionString);
                await dbContext.Database.MigrateAsync();
            }
        }
        /// <summary>
        /// Add new repository to DI
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation">must inherit Repository base class and must implement TService</typeparam>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepository<TService, TImplementation>(this IServiceCollection services, string connectionString) where TService : class where TImplementation : class, TService
        {
            return services.AddScoped<TService, TImplementation>(x => (TImplementation)Activator.CreateInstance(typeof(TImplementation), new object[] { connectionString })!);
        }
    }
}

