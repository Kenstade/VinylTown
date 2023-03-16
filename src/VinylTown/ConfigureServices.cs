using MediatR;
using Microsoft.EntityFrameworkCore;
using VinylTown.Data;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("VinylTownDb"));
        }
        else
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        services.AddMediatR(typeof(Program));
        


        services.AddControllersWithViews();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerDocument();

        return services;
    }
}
