using Microsoft.AspNetCore.Mvc;
using App.Core.Query;
using App.Models.Entities;
using App.Services;

namespace App.Web.Controllers;

[Route("api/[controller]")]
public class ItemController : EntityController<Item>
{
	readonly ItemService itemSvc;

	public ItemController(ItemService svc) : base(svc)
	{
		itemSvc = svc;
	}

	[HttpGet("[action]/{categoryId}")]
	public async Task<IActionResult> QueryItems(
		[FromRoute]int categoryId, [FromQuery]QueryParams queryParams
	) => Ok(await itemSvc.QueryItems(categoryId, queryParams));
}