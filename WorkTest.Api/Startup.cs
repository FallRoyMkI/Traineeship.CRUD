using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WorkTest.Api.Controllers.Extension;
using WorkTest.Api.MapperProfiles;
using WorkTest.Api.Middleware;
using WorkTest.Dal;

namespace WorkTest.Api;

public class Startup
{
    private string _connectionString;
    public IConfiguration Configuration { get; }


    public Startup(IConfiguration configuration)
    {
        IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        _connectionString = configuration.GetConnectionString("DataBase");
        Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MapperOrderProfile), typeof(MapperProductProfile));
        services.AddCustomRepositories();
        services.AddCustomServices();

        services.AddDbContext<Context>(x => x.UseNpgsql(_connectionString));

        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });
        services.AddMvc();

        services.AddOptions();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        app.UseMiddleware<ExceptionMiddleware>();

        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        app.Migration();
    }
}