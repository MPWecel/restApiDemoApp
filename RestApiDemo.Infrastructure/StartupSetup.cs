using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestApiDemo.Infrastructure.Data;

namespace RestApiDemo.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<AppDbContext>(
                options =>
                {
                    //options.UseSqlite(connectionString)
                    options.UseSqlServer(connectionString);
                }
            );
    }
}
