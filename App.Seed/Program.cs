using App.Seed;
using App.Services;

try
{
	string env = args.Length > 0
				? args[0]
				: "Dev";

	using DbGenerator generator = new (env);
	await generator.InitializeAsync();

	Console.WriteLine("Seeding Categories...");
	CategoryService categorySvc = new(generator.Context);
	await categorySvc.Seed();

	Console.WriteLine("Seeding Items...");
	ItemService itemSvc = new(generator.Context);
	await itemSvc.Seed();

	Console.WriteLine("Database seeding completed successfully");
}
catch (Exception ex)
{
	throw new Exception("An error occurred while seeding the database", ex);
}