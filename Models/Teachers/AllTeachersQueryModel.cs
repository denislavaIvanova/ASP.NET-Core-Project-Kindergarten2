namespace Kindergarten2.Models.Teachers
{
	using System.Collections.Generic;

	public class AllTeachersQueryModel
	{

		public IEnumerable<string> Specializations { get; init; }

		public IEnumerable<string> SearchTerm { get; init; }

		public TeacherSorting Sorting { get; init; }

		public IEnumerable<TeacherListingViewModel> Teachers { get; init; }
	}
}
