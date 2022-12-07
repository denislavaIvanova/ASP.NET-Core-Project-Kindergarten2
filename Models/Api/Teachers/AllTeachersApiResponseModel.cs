
namespace Kindergarten2.Models.Api.Teachers
{
	using System.Collections.Generic;

	public class AllTeachersApiResponseModel
	{
		public int CurrentPage { get; init; }

		public int TeachersPerPage { get; init; }

		public int TotalTeachers { get; init; }

		public IEnumerable<TeacherResponseModel> Teachers { get; init; }
	}
}
