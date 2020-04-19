using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DzShopping.API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentations(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "DzShopping API",
                    Description = "A simple example ASP.NET Core Web API"
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentations(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My DzShopping API V1");
                c.RoutePrefix = "swagger/ui";
            });


            return app;
        }
    }
}
