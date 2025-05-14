// Importing necessary namespaces for database connection, dependency injection, and services
using Microsoft.EntityFrameworkCore;
using TodoAPi.Context;
using TodoAPi.Exceptions;
using TodoAPi.Interface;
using TodoAPi.Service;

namespace TodoAPi.Extensions
{
    // Defining a static class for service extensions to configure dependency injection
    public static class ServiceExtensions
    {
        // Extension method to register application-specific services
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            // Configuring the database context to use SQL Server with a connection string from app settings
            builder.Services.AddDbContext<ToDODbcontext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
            });

            // Registering the ITodoServices interface with its concrete implementation TodoServices
            builder.Services.AddScoped<ITodoServices, TodoServices>();

            // Adding global exception handling using a custom exception handler
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            // Enabling Problem Details middleware for detailed error responses
            builder.Services.AddProblemDetails();

            // Registering AutoMapper to handle object mappings between different models and DTOs
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
