using Microsoft.OpenApi.Models;

namespace Talabat.Extensions
{
    public static class SwaggerServicesGen
    {
          public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
          {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Talabat.ApI", Version = "V1" });
                var securityScheme = new OpenApiSecurityScheme()
                {

                    Description = "JWT Auth Bearer Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id ="Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securityScheme);

                var securityRequirement = new OpenApiSecurityRequirement { { securityScheme, new[] { "Bearer" } } };
                c.AddSecurityRequirement (securityRequirement);
                
            }); 

            return services;

          }
    }
}
