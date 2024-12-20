﻿using System;

using CodeChallenge.Data;
using CodeChallenge.Repositories;
using CodeChallenge.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeChallenge.Config
{
    public class App
    {
        public WebApplication Configure(string[] args)
        {
            args ??= Array.Empty<string>();

            var builder = WebApplication.CreateBuilder(args);

            builder.UseEmployeeDB();

            AddServices(builder.Services);

            var app = builder.Build();

            var env = builder.Environment;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedEmployeeDB();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeChallenge API V1");
                    c.RoutePrefix = string.Empty;
                });

                app.UseAuthorization();

                app.MapControllers();

            }
            return app;
        }

        private void AddServices(IServiceCollection services)
            {

                services.AddScoped<IEmployeeService, EmployeeService>();
                services.AddScoped<IEmployeeRepository, EmployeeRepository>();
                services.AddScoped<IReportingStructureService, ReportingStructureService>();
                services.AddControllers();
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen();
            }

            private void SeedEmployeeDB()
            {
                new EmployeeDataSeeder(
                    new EmployeeContext(
                        new DbContextOptionsBuilder<EmployeeContext>().UseInMemoryDatabase("EmployeeDB").Options
                    )).Seed().Wait();
            }
        }
}
