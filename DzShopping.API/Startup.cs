using AutoMapper;
using DzShopping.API.AutoMapper;
using DzShopping.API.Extensions;
using DzShopping.API.Middleware;
using DzShopping.Infrastructure.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

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

            // Add redis for saving cart items
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var configuration = ConfigurationOptions.Parse(_configuration.GetSection("DzShopping")["Redis"], true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddCors();

            services.AddAutoMapper(typeof(MappingProfiles));


            services.AddSwaggerDocumentations();
            services.AddApplicationServices();
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

            app.UseSwaggerDocumentations();
        }
    }
}