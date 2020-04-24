﻿using AutoMapper;
using CarWashPOI.Data;
using CarWashPOI.Services.Images;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CarWashPOITests
{
    public class DependenciesResolver
    {
        private readonly ServiceProvider serviceProvider;

        public DependenciesResolver()
        {
            var serviceCollection = new ServiceCollection();

            // Add configuration
            var configurtaionBuilder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .AddEnvironmentVariables();
            IConfiguration configuration = configurtaionBuilder.Build();
            serviceCollection.AddSingleton(configuration);

            // Add in memory db
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()), ServiceLifetime.Transient);

            // Add AutoMapper
            MapperConfiguration mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperMappings());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            serviceCollection.AddSingleton(mapper);

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return serviceProvider.GetService<T>();
        }
    }
}
