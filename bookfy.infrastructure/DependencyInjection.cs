using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bookfy.infrastructure
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddDependencies(IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}