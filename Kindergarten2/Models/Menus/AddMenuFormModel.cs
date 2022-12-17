
namespace Kindergarten2.Models.Menus
{
	using System.ComponentModel.DataAnnotations;

	public class AddMenuFormModel
	{

		[Required]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "The {0} must be between {2} and {1} symbols.")]
		[Display(Name = "Menu type")]

		public string MenuType { get; init; }

		[Required]
		[StringLength(300, MinimumLength = 2, ErrorMessage = "The {0} must be between {2} and {1} symbols.")]

		public string Description { get; init; }

		[Range(0, 2000)]
		[Display(Name = "Price per menu in EUR")]

		public double Price { get; init; }

		[Required]
		[Url]
		[Display(Name = "Image URL")]
		public string ImageUrl { get; init; }

	}
}
