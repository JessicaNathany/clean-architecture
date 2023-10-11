using bookfy.domain.Bookings;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace bookfy.application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
            });

            services.AddTransient<PriceService>();

            return services;
        }
    }
}
