
namespace Kindergarten2.Data.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	public class ECA
	{
		public int Id { get; init; }

		[Required]
		[MaxLength(300)]
		public string Description { get; set; }

		public double MonthlyFee { get; set; }

		public IEnumerable<Child> Children { get; init; } = new List<Child>();
	}
}
