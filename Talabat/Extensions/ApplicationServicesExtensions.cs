using Microsoft.AspNetCore.Mvc;
using Talabat.BLL.Interfaces;
using Talabat.BLL.Repositories;
using Talabat.BLL.Services;
using Talabat.Errors;
using Talabat.Helper;

namespace Talabat.Extensions
{
    public static class ApplicationservicesExtensions
    {
        
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(x => x.Value.Errors.Count > 0)
                                         .SelectMany(x => x.Value.Errors).Select(e => e.ErrorMessage)
                                         .ToArray();
                    var errorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                     };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}
