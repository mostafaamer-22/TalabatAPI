using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Talabat.BLL.Interfaces;
using Talabat.BLL.Repositories;
using Talabat.BLL.Services;
using Talabat.DAL.Data;
using Talabat.DAL.Entities.Identity;
using Talabat.DAL.Identity;
using Talabat.Extensions;
using Talabat.Helper;
using Talabat.MiddleWares;

namespace Talabat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<StoreContext>(Options => {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DeflautConnection"));
            });

     

            builder.Services.AddDbContext<AppIdentityContextDb>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer> (options =>
            {
                var connection = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(connection);
            });
            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddSwaggerDocumentation();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExecptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseHttpsRedirection();

            SeedData();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            async void SeedData()
            {
                using(var Scope = app.Services.CreateScope())
                {
                    var serviceProvider = Scope.ServiceProvider;
                    var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                    try
                    {
                        var context = serviceProvider.GetRequiredService<StoreContext>();
                        await context.Database.MigrateAsync();
                        await StoreContextSeed.SeedAsync(context , loggerFactory);
                        var identityContext = serviceProvider.GetRequiredService<AppIdentityContextDb>();
                        await identityContext.Database.MigrateAsync();
                        var userManger = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                        await AppIdentityDbContextSeed.SeedUserAsync(userManger);

                    }
                    catch (Exception ex)
                    {
                        var Logger = loggerFactory.CreateLogger<Program>();
                        Logger.LogError(ex.Message);
                    }
                }
            }
        }
    }
}