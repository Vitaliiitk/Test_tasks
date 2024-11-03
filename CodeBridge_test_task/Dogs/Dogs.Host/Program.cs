using AspNetCoreRateLimit;
using Dogs.Host.Data;
using Dogs.Host.Mapping;
using Dogs.Host.Repositories;
using Dogs.Host.Repositories.Interfaces;
using Dogs.Host.Services;
using Dogs.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var configuration = GetConfiguration();

var builder = WebApplication.CreateBuilder(args);

// Add AspNetCoreRateLimit
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "Dog House- Dogs API",
		Version = "v1",
		Description = "The Dog Service HTTP API"
	});

	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	options.IncludeXmlComments(xmlPath);
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddTransient<IDogsHouseRepository, DogsHouseRepository>();
builder.Services.AddTransient<IDogsHouseService, DogsHouseService>();
builder.Services.AddTransient<IDogRepository, DogRepository>();
builder.Services.AddTransient<IDogService, DogService>();

builder.Services.AddLogging();

builder.Services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

var app = builder.Build();

app.UseIpRateLimiting();

app.UseRouting();
app.UseSwagger()
	.UseSwaggerUI(setup =>
{
	setup.SwaggerEndpoint($"/swagger/v1/swagger.json", "Dogs.API V1");
	setup.OAuthClientId("dogsswaggerui");
	setup.OAuthAppName("Dogs Swagger UI");
});

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});

CreateDbIfNotExists(app);
app.Run();

IConfiguration GetConfiguration()
{
	var builder = new ConfigurationBuilder()
		.SetBasePath(Directory.GetCurrentDirectory())
		.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
		.AddEnvironmentVariables();

	return builder.Build();
}

async Task CreateDbIfNotExists(IHost host)
{
	using (var scope = host.Services.CreateScope())
	{
		var services = scope.ServiceProvider;
		try
		{
			var context = services.GetRequiredService<ApplicationDbContext>();

			await DbInitializer.Initialize(context);
		}
		catch (Exception ex)
		{
			var logger = services.GetRequiredService<ILogger<Program>>();
			logger.LogError(ex, "An error occurred creating the DB.");
		}
	}
}

