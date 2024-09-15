using Microsoft.EntityFrameworkCore;
using testTaskReenbit.Data;
using testTaskReenbit.Hubs;
using testTaskReenbit.TextAnalyticsAPI;

namespace testTaskReenbit
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddSingleton<TextAnalyticsService>();
			builder.Services.AddSignalR().AddAzureSignalR(options =>
			{
				options.ConnectionString = builder.Configuration.GetConnectionString("AzureSignalRConnectionString");
			});

			builder.Services.AddRazorPages();

			builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
					sqlOptions => sqlOptions.EnableRetryOnFailure(
					maxRetryCount: 5,
					maxRetryDelay: TimeSpan.FromSeconds(30),
					errorNumbersToAdd: null)));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseDefaultFiles();
			app.UseRouting();
			app.MapHub<ChatHub>("/chat");


			app.MapRazorPages();

			app.Run();
		}
	}
}