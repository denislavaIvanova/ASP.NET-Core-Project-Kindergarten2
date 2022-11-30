
namespace Kindergarten2.Models.Menus
{
	using System.ComponentModel.DataAnnotations;

	public class AddMenuFormModel
	{

		[Required]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "The {0} must be between {2} and {1} symbols.")]
		[Display(Name = "Menu type")]

		public string MenuType { get; init; }

		[Range(0, 2000)]
		[Display(Name = "Price per menu in EUR(0.00)")]

		public double Price { get; init; }

	}
}
