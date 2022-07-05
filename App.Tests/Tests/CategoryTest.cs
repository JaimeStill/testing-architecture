using App.Models.Entities;
using App.Services;
using App.Tests.Fixtures;

namespace App.Tests;
public class CategoryTest : TestBase<Category, CategoryService>
{
	public CategoryTest(ServiceFixture<Category, CategoryService> fixture) : base(fixture) { }

	[Theory]
	[MemberData(nameof(Data))]
	public async Task Save(Category entity)
	{
		string update = $"Updated {entity.Value}";

		var res = await Svc.Save(entity);
		Assert.InRange(res.Id, 1, int.MaxValue);

		entity.Value = update;
		await Svc.Save(entity);
		res = await Svc.Find(res.Id);
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