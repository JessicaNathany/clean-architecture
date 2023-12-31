﻿using bookfy.application.Abstractions.Clock;
using bookfy.application.Abstractions.Email;
using bookfy.infrastructure.Clock;
using bookfy.infrastructure.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bookfy.infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            return services;
        }
    }
}