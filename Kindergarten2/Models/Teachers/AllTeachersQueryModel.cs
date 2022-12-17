namespace Kindergarten2.Models.Teachers
{
	using Kindergarten2.Services.Teachers;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class AllTeachersQueryModel
	{
		public const int TeachersPerPage = 3;
		public string Specialization { get; init; }
		public IEnumerable<string> Specializations { get; set; }

		[Display(Name = "Search")]
		public string SearchTerm { get; init; }

		public TeacherSorting Sorting { get; init; }

		//current page if IT HAS NO VALUE, IT WILL START FROM 1
		public int CurrentPage { get; init; } = 1;

		public int TotalTeachers { get; set; }
		public IEnumerable<TeacherServiceModel> Teachers { get; set; }
	}
}
