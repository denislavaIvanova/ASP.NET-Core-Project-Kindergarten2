
namespace Kindergarten2.Models.Teachers
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class AddTeacherFormModel
	{
		[Required]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} symbols.")]
		[Display(Name = "First name")]
		public string FirstName { get; init; }

		[Required]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} symbols.")]
		[Display(Name = "Last name")]
		public string LastName { get; init; }

		[Range(0, 100)]
		public int Experience { get; init; }

		[Required]
		[StringLength(300, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} symbols.")]

		public string Specialization { get; init; }

		[Display(Name = "Group")]
		public int GroupId { get; init; }

		public IEnumerable<TeacherGroupViewModel> Groups { get; set; }

	}
}
