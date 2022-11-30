
namespace Kindergarten2.Models.ECAs
{
	using System.ComponentModel.DataAnnotations;

	public class AddECAFormModel
	{
		[Required]
		[StringLength(300, MinimumLength = 2, ErrorMessage = "The {0} must be between {2} and {1} symbols.")]
		public string Description { get; init; }

		[Display(Name = "Monthly Fee in EUR")]
		[Range(0, 10000)]
		public double MonthlyFee { get; init; }

		//public IEnumerable<Child> Children { get; init; } = new List<Child>();
	}
}
