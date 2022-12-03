namespace Kindergarten2.Models.Teachers
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class AllTeachersQueryModel
	{
		public string Specialization { get; init; }
		public IEnumerable<string> Specializations { get; init; }

		[Display(Name = "Search")]
		public string SearchTerm { get; init; }

		public TeacherSorting Sorting { get; init; }

		public IEnumerable<TeacherListingViewModel> Teachers { get; init; }
	}
}
