
namespace Kindergarten2.Data.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class Parent
	{
		public int Id { get; init; }

		[Required]
		[MaxLength(25)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(25)]

		public string LastName { get; set; }

		[Required]
		[MaxLength(35)]

		public string PhoneNumber { get; set; }

		[Required]
		public string UserId { get; set; }

		public IEnumerable<Child> Children { get; init; } = new List<Child>();

	}
}
