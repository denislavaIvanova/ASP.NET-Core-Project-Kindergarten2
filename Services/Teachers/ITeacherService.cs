
namespace Kindergarten2.Services.Teachers
{
	using Kindergarten2.Models.Teachers;
	using System.Collections.Generic;

	public interface ITeacherService
	{
		TeacherQueryServiceModel All(string specialization,
			string searchTerm,
			TeacherSorting sorting,
			int currentPage,
			int teachersPerPage);

		IEnumerable<string> AllTeacherSpecializations();
	}


}
