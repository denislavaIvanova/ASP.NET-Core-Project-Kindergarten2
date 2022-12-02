namespace Kindergarten2.Models.Trips
{
	using System.ComponentModel.DataAnnotations;

	public class AddTripFormModel
	{

		[Required]
		[StringLength(300, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} symbols.")]
		[Display(Name = "Place To Visit")]
		public string PlaceToVisit { get; set; }

		[Required]
		[Url]
		[Display(Name = "Image URL")]
		public string ImageUrl { get; init; }

		[Required]
		[StringLength(300, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} symbols.")]
		public string Activity { get; set; }

		[Range(0, 10000)]
		[Display(Name = "Price in EUR")]

		public double Price { get; set; }

		[Required]
		[StringLength(300, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} symbols.")]
		public string Location { get; set; }
	}
}
