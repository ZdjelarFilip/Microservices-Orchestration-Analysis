using CartManagementService.Extensions;
using JwtMongoMiddleware.Configurations;

namespace CartManagementService
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();            
            services.ConfigureMongoDb(Configuration);
            services.ConfigureJwtAuthentication(Configuration);
            services.ConfigureDependencyInjection();
            services.ConfigureSwagger();
            services.ConfigureCors();
            services.ConfigureHealthChecks(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.ConfigureMiddleware();
            app.ConfigureSwaggerUIAndRewrites();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ConfigureCustomHealthChecks();
        }
    }
}
