using App.Models.Entities;
using App.Services;

namespace App.Tests;
public class CategoryTest : TestBase<Category, CategoryService>
{
	public CategoryTest() : base()
	{
		svc = new CategoryService(manager.Context);
		svc.SeedTest().Wait();

	}

	[Theory]
	[MemberData(nameof(Data))]
	public async Task Save(Category entity)
	{
		string update = "Updated Entity";

		var res = await svc.Save(entity);
		Assert.InRange(res.Id, 1, int.MaxValue);

		entity.Value = update;
		await svc.Save(entity);
		res = await svc.Find(res.Id);
		Assert.Equal(res.Value, update);
	}

	public static IEnumerable<object[]> Data =>
		new List<object[]>
		{
			new object[] { new Category { Value = "Test A" }},
			new object[] { new Category { Value = "Test B" }},
			new object[] { new Category { Value = "Test C" }}
		};
}