using Microsoft.EntityFrameworkCore;
using App.Core.Exceptions;
using App.Data;
using App.Models.Entities;

namespace App.Services;
public class CategoryService : ServiceBase<Category>
{
	public CategoryService(AppDbContext db) : base(db) { }

	protected override Func<IQueryable<Category>, string, IQueryable<Category>> Search =>
		(values, term) =>
			values.Where(x => x.Value.ToLower().Contains(term.ToLower()));

	protected async Task<bool> ValidateRecord(Category entity) =>
		!await set.AnyAsync(x =>
			x.Id != entity.Id
			&& x.Value.ToLower() == entity.Value.ToLower()
		);

	public override async Task<bool> Validate(Category entity)
	{
		if (string.IsNullOrWhiteSpace(entity.Value))
			throw new AppException("Category must have a value", ExceptionType.Validation);

		if (!await ValidateRecord(entity))
			throw new AppException("The provided Category already exists", ExceptionType.Validation);

		return true;
	}

	public override async Task Seed()
	{
		await db.Categories.AddRangeAsync(new List<Category>
		{
			new Category { Value = "Test" },
			new Category { Value = "Demo" }
		});

		await db.SaveChangesAsync();
	}

	public override Task SeedTest() => Seed();
}