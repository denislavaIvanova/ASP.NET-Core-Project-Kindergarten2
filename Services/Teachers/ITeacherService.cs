
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

		int Create(int groupId,
			string firstName,
			string lastName,
			int experience,
			string specialization,
			string introduction,
			string imageUrl);

		bool GroupExist(int groupId);

		IEnumerable<TeacherGroupServiceModel> GetTeacherGroups();
	}


}
