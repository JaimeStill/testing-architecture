using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using App.Data;

namespace App.Seed;
public class DbGenerator : IDisposable
{
	readonly bool destroy;
	public AppDbContext Context { get; private set; }

	static string GetConnectionString(string env)
	{
		IConfiguration config = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.AddEnvironmentVariables()
			.Build();

		string connection = config.GetConnectionString(env);
		Console.WriteLine($"Connection string: {connection}");

		return connection;
	}

	static AppDbContext GetDbContext(string connection)
	{
		var builder = new DbContextOptionsBuilder<AppDbContext>()
			.UseSqlServer(connection);

		return new AppDbContext(builder.Options);
	}

	public DbGenerator(string env = "Dev", bool destroy = false)
	{
		this.destroy = destroy;
		Context = GetDbContext(GetConnectionString(env));
	}

	public void Initialize()
	{
		Console.WriteLine("Initializing database");
		if (destroy)
			Context.Database.EnsureDeleted();

		Context.Database.Migrate();
		Console.WriteLine("Database initialized");
	}

	public async Task InitializeAsync()
	{

		Console.WriteLine("Initializing database");
		if (destroy)
			await Context.Database.EnsureDeletedAsync();

		await Context.Database.MigrateAsync();
		Console.WriteLine("Database initialized");
	}

	public void Dispose()
	{
		if (destroy)
			Context.Database.EnsureDeleted();

		Context.Dispose();
		GC.SuppressFinalize(this);
	}


}