using bookfy.application.Abstractions.Behaviors;
using bookfy.domain.Bookings;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace bookfy.application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
                configuration.AddBehavior(typeof(LogginBehavior<,>));
                configuration.AddBehavior(typeof(ValidationBehavior<,>));
            });

            // services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly); corrigir referencia
            services.AddTransient<PricingService>();

            return services;
        }
    }
}
