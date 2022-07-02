using App.Core.Query;
using App.Models;
using App.Seed;

namespace App.Tests;
public abstract class TestBase<T, S> : IDisposable
	where T : EntityBase
	where S : IService<T>
{
	protected DbGenerator generator;
	protected S svc;

	public TestBase()
	{
		generator = new("Test", true);
		generator.Initialize();
	}

	public virtual void Dispose()
	{
		generator.Dispose();
		GC.SuppressFinalize(this);
	}

	#region Tests

	[Fact]
	public async Task QueryAll()
	{
		var res = await svc.QueryAll(new QueryParams());
		Assert.NotEmpty(res.Data);
	}

	[Theory]
	[InlineData(1)]
	[InlineData(2)]
	public async Task Find(int id)
	{
		var res = await svc.Find(id);
		Assert.Equal(id, res.Id);
	}

	#endregion
}