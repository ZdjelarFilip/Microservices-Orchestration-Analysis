using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;
using OrderManagementService.Messages;
using OrderManagementService.Middleware;
using OrderManagementService.Repositories;
using OrderManagementService.Services;

namespace OrderManagementService.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSwaggerAndSecurity(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OrderManagement Microservice" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 123abc'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "Bearer",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        BearerFormat = "JWT",
                        Type = SecuritySchemeType.ApiKey,
                    },
                    new List<string>()
                }
                });
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
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderManagement API"));

            var options = new RewriteOptions();
            options.AddRedirect("^$", "swagger");
            app.UseRewriter(options);
        }

        public static void ConfigureHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            string cartUrl = configuration["HttpClients:CartManagementUrl"] ?? throw new ArgumentNullException("HttpClients:CartManagementUrl", "Cart Management URL configuration is missing.");
            services.AddHttpClient("CartClient", client =>
            {
                client.BaseAddress = new Uri(cartUrl + "CartManagement");
            });
        }

        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICartManagementRepository, CartManagementRepository>();
            services.AddScoped<IOrderManagementRepository, OrderManagementRepository>();
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