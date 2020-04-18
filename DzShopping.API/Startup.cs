using System.Linq;
using AutoMapper;
using DzShopping.API.AutoMapper;
using DzShopping.API.Errors;
using DzShopping.API.Middleware;
using DzShopping.Infrastructure.DbContext;
using DzShopping.Infrastructure.Repositories.GenericRepository;
using DzShopping.Infrastructure.Repositories.ProductBrandRepository;
using DzShopping.Infrastructure.Repositories.ProductRepository;
using DzShopping.Infrastructure.Repositories.ProductTypeRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace DzShopping.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<DzDbContext>(opt =>
                opt.UseSqlServer(_configuration.GetSection("DzShopping")["ConnStr"]));
            services.AddCors();

            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IProductBrandRepository, ProductBrandRepository>();

            // This for generic repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Nuget package enable retrieving long Jsons
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "DzShopping API",
                    Description = "A simple example ASP.NET Core Web API"
                });
            });


            //To handle endpoint does not exist error request
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //My own custom ExceptionMiddleware(catch errors then send my custom errors massages   )
            app.UseMiddleware<ExceptionMiddleware>();

            //Get generic error based on error number like 400 bad request, they are living in ApiResponse class
            //Then execute in ErrorController
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:4200"));
            app.UseHttpsRedirection();

            app.UseRouting();

            //To activate wwwroot files(I am saving images under wwwroot that I can get in postman)
            //Needs to be after app.UseRouting()
            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My DzShopping API V1");
                c.RoutePrefix = "swagger/ui";
            });
        }
    }
}