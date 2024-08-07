namespace AdminUI;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddCors(options =>
        {
            /*
             *             options.AddPolicy("CorsPolicy",
                builder => builder.WithOrigins(Configuration["HttpClients:CartManagementUrl"],
                                               Configuration["HttpClients:CatalogManagementUrl"],
                                               Configuration["HttpClients:OrderManagementUrl"],
                                               Configuration["HttpClients:UserAuthenticationUrl"])
                    .AllowAnyMethod()
                    .AllowAnyHeader());*/

            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseCors("CorsPolicy");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseDefaultFiles();
        app.UseStaticFiles();
    }
}