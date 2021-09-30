
using DealerTrack.DealManagement.Application.Contracts.Infrastructure;
using DealerTrack.DealManagement.Infrastructure.FileReader;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;


namespace DealerTrack.DealManagement.Infrastructure
{
    

    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
           

            //services.AddTransient<IFileReaderFactory, FileReaderFactory>();
           services.AddTransient<IFileImporter, CSVReader>();


            return services;
        }
    }
}
