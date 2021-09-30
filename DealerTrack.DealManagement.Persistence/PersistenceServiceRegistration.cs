
using DealerTrack.DealManagement.Application.Contracts.Persistence;
using DealerTrack.DealManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DealerTrack.DealManagement.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DealManagementDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DealManagementConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IDealRepository, DealRepository>();
            

            return services;
        }
    }
}
