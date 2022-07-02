using Microsoft.Extensions.DependencyInjection;

namespace App.Services;
public static class ServiceExtensions
{
	public static void AddAppServices(this IServiceCollection services)
	{
		// services.AddScoped<Service>();
		services.AddScoped<CategoryService>();
		services.AddScoped<ItemService>();
	}
}