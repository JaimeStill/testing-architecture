using Microsoft.EntityFrameworkCore;
using App.Core.Exceptions;
using App.Core.Query;
using App.Data;
using App.Models.Entities;

namespace App.Services;
public class ItemService : ServiceBase<Item>
{
	public ItemService(AppDbContext db) : base(db) { }

	protected override Func<IQueryable<Item>, string, IQueryable<Item>> Search =>
		(values, term) =>
			values.Where(x =>
				x.Name.ToLower().Contains(term.ToLower())
				|| x.Category.Value.ToLower().Contains(term.ToLower())
			);

	protected async Task<bool> ValidateRecord(Item entity) =>
		!await set.AnyAsync(x =>
			x.Id != entity.Id
			&& x.CategoryId == entity.CategoryId
			&& x.Name.ToLower() == entity.Name.ToLower()
		);

	public async Task<QueryResult<Item>> QueryItems(int categoryId, QueryParams queryParams) =>
		await Query(
			set.Where(x => x.CategoryId == categoryId),
			queryParams, Search
		);

	public override async Task<bool> Validate(Item entity)
	{
		if (entity.CategoryId < 1)
			throw new AppException("An Item must have a Category", ExceptionType.Validation);

		if (string.IsNullOrWhiteSpace(entity.Name))
			throw new AppException("An Item must have a Name", ExceptionType.Validation);

		if (!await ValidateRecord(entity))
			throw new AppException("The provided Item already exists", ExceptionType.Validation);

		return true;
	}

	public override async Task Seed()
	{
		var category = await db.Categories.FirstOrDefaultAsync(x => x.Value == "Demo");

		if (category is null)
		{
			category = new Category { Value = "Demo" };
			await db.Categories.AddAsync(category);
		}

		await db.Items.AddRangeAsync(new List<Item>
		{
			new Item { Name = "Item A", Category = category },
			new Item { Name = "Item B", Category = category }
		});

		await db.SaveChangesAsync();
	}

	public override Task SeedTest() => Seed();
}