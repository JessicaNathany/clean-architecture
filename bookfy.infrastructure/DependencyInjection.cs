using bookfy.application.Abstractions.Clock;
using bookfy.application.Abstractions.Data;
using bookfy.application.Abstractions.Email;
using bookfy.domain.Abstractions;
using bookfy.domain.Apartaments;
using bookfy.domain.Bookings;
using bookfy.domain.Users;
using bookfy.infrastructure.Clock;
using bookfy.infrastructure.Data;
using bookfy.infrastructure.Email;
using bookfy.infrastructure.Repositories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bookfy.infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            var connectionsString = configuration.GetConnectionString("Database") ??
                throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(otions =>
            {
                otions.UseNpgsql(connectionsString).UseSnakeCaseNamingConvention();
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IApartamentRepository, ApartmentRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionsString));

            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

            return services;
        }
    }
}