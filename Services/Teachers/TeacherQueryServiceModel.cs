using System.Collections.Generic;

namespace Kindergarten2.Services.Teachers
{
	public class TeacherQueryServiceModel
	{
		public int CurrentPage { get; init; }

		public int TeachersPerPage { get; init; }

		public int TotalTeachers { get; init; }

		public IEnumerable<TeacherServiceModel> Teachers { get; init; }
	}
}
