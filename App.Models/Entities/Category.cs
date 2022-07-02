namespace App.Models.Entities;
public class Category : EntityBase
{
	public string Value { get; set; }

	public ICollection<Item> Items { get; set; }
}