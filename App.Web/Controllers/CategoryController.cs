using Microsoft.AspNetCore.Mvc;
using App.Models.Entities;
using App.Services;

namespace App.Web.Controllers;

[Route("api/[controller]")]
public class CategoryController : EntityController<Category>
{
	public CategoryController(CategoryService svc) : base(svc) { }
}