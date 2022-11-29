
namespace Kindergarten2.Models.Teachers
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class AddTeacherFormModel
	{
		[Required]
		[MinLength(2)]
		[MaxLength(100)]
		[Display(Name = "First name")]
		public string FirstName { get; init; }

		[Required]
		[MinLength(2)]
		[MaxLength(100)]
		[Display(Name = "Last name")]
		public string LastName { get; init; }

		[Range(0, 100)]
		public int Experience { get; init; }

		[Required]
		[MinLength(2)]
		[MaxLength(300)]
		public string Specialization { get; init; }

		[Display(Name = "Group")]
		public int GroupId { get; init; }

		public IEnumerable<TeacherGroupViewModel> Groups { get; set; }

	}
}
