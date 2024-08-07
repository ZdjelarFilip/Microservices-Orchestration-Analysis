using HealthChecks.UI.Client;
using JwtMongoMiddleware.CyptoMiddleware;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;
using UserAuthenticationService.Messages;
using UserAuthenticationService.Middleware;
using UserAuthenticationService.Repositories;

namespace UserAuthenticationService.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserAuthentication API" });
            });
        }

        public static void ConfigureSwaggerAndErrorHandling(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exceptionFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                if (exceptionFeature != null)
                {
                    var exception = exceptionFeature.Error;
                    var response = new { Error = exception.Message };
                    await context.Response.WriteAsJsonAsync(response);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsJsonAsync(new { Error = ErrorMessages.InvalidConfiguration });
                }
            }));

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserAuthentication API"));

            var options = new RewriteOptions();
            options.AddRedirect("^$", "swagger");
            app.UseRewriter(options);
        }

        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IJwtTokenFactory, JwtTokenFactory>();
            services.AddScoped<ICryptoSaltHasher, CryptoSaltHasher>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserAuthenticationRepository, UserAuthenticationRepository>();        
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }

        public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            string mongoConnectionString = configuration["MongoDatabaseSettings:DatabaseUri"]
                ?? throw new ArgumentNullException("MongoDatabaseSettings:DatabaseUri",
                                                   "DatabaseUri for MongoDatabaseSettings is missing.");

            services.AddHealthChecks()
                .AddMongoDb(mongoConnectionString, "MongoDatabaseSettings", Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy);
            services.AddHealthChecksUI().AddInMemoryStorage();
        }

        public static void ConfigureCustomHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.UseHealthChecksUI();
        }

        public static void ConfigureMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}