
namespace Kindergarten2.Models.Api.Teachers
{
	using Kindergarten2.Models.Teachers;

	public class AllTeachersApiRequestModel
	{
		public string Specialization { get; init; }

		public string SearchTerm { get; init; }

		public TeacherSorting Sorting { get; init; }

		public int CurrentPage { get; init; } = 1;

		public int TeachersPerPage { get; init; } = 10;

	}
}
