namespace App.Models.Entities;
public class Item : EntityBase
{
	public int CategoryId { get; set; }
	public string Name { get; set; }

	public Category Category { get; set; }
}