namespace Kindergarten2.Data.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	public class Trip
	{
		public int Id { get; init; }

		[Required]
		[MaxLength(300)]
		public string PlaceToVisit { get; set; }

		[Required]
		[MaxLength(300)]
		public string Activity { get; set; }

		[Required]
		public string ImageUrl { get; set; }

		public double Price { get; set; }

		[Required]
		[MaxLength(300)]
		public string Location { get; set; }
		public IEnumerable<Child> Children { get; init; } = new List<Child>();
	}
}
