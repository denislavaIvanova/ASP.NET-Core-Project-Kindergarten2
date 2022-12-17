
namespace Kindergarten2.Models.Home
{
	using Kindergarten2.Services.Teachers;
	using System.Collections.Generic;

	public class IndexViewModel
	{
		public int TotalTeachers { get; init; }

		public int TotalChildren { get; init; }

		public int TotalGroups { get; init; }

		public List<LatestTeacherServiceModel> Teachers { get; init; }

	}
}
