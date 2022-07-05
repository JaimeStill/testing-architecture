using App.Core.Query;
using App.Models;
using App.DbCli;

namespace App.Tests;
public abstract class TestBase<T, S> : IDisposable
	where T : EntityBase
	where S : IService<T>
{
	protected DbManager manager;
	protected S svc;

	public TestBase()
	{
		manager = new("Test", true);
		manager.Initialize();
	}

	public virtual void Dispose()
	{
		manager.Dispose();
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

	[Theory]
	[InlineData(1)]
	[InlineData(2)]
	public async Task Remove(int id)
	{
		var entity = await svc.Find(id);
		var res = await svc.Remove(entity);
		Assert.True(res);
	}

	#endregion
}