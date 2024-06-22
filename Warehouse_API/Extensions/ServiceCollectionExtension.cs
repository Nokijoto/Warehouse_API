using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Warehouse_API.Interfaces;
using Warehouse_API.Interfaces.IServices;
using Warehouse_API.Services;

namespace Warehouse_API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddWarehouseServices(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddScoped<IProductService, ProductService>();
            serviceCollection.AddScoped<IRfidService,RfidService>();



            serviceCollection.AddDbContext<WarehouseDbContext>(options =>
            {
                options.UseSqlServer(serviceCollection.BuildServiceProvider().GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection"));
            });

            return serviceCollection;
        }

        public static IServiceCollection AddAuthenticationCollection(this IServiceCollection serviceCollection)
        {
            var key = Encoding.UTF8.GetBytes("projekt_magazynu_na_inzynierie_oprogramowania_lab");
            serviceCollection.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "ABCXYZ",
                    ValidAudience = "http://localhost:51398",
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            return serviceCollection;
        }
    }
}
