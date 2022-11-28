namespace Kindergarten2.Data.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	public class Category
	{
		public int Id { get; init; }

		[Required]
		[MaxLength(100)]
		public string Description { get; set; }
		public IEnumerable<Group> Groups { get; init; } = new List<Group>();
	}
}
