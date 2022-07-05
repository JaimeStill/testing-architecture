using App.DbCli;
using App.Services;

try
{
	string env = args.Length > 0
		? args[0]
		: "Dev";

	using DbManager manager = new(env);
	await manager.InitializeAsync();

	Console.WriteLine("Seeding Categories...");
	CategoryService categorySvc = new(manager.Context);
	await categorySvc.Seed();

	Console.WriteLine("Seeding Items...");
	ItemService itemSvc = new(manager.Context);
	await itemSvc.Seed();

	Console.WriteLine("Database seeding completed successfully");
}
catch (Exception ex)
{
	throw new Exception("An error occurred while seeding the database", ex);
}