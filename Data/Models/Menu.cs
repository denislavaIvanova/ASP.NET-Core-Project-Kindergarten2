namespace Kindergarten2.Data.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class Menu
	{
		public int Id { get; init; }

		[Required]
		[MaxLength(100)]

		public string MenuType { get; set; }

		[Required]
		[MaxLength(300)]

		public string Description { get; set; }

		public double Price { get; set; }

		[Required]
		public string ImageUrl { get; set; }

		public IEnumerable<Child> Children { get; init; } = new List<Child>();
	}
}
