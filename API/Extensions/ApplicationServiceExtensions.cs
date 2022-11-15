using API.Data;
using API.Models;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationService(this IServiceCollection serviceCollection, IConfiguration config)
    {
        serviceCollection.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });

        serviceCollection.AddScoped<ITokenService, TokenService>();

        return serviceCollection;
    }
    
}