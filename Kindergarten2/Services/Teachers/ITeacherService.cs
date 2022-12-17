
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

		List<LatestTeacherServiceModel> Latest();

		IEnumerable<TeacherGroupServiceModel> GetTeacherGroups();

		TeacherDetailsServiceModel Details(int id);

		bool Edit(int id,
			string firstName,
				string lastName,
				string specialization,
				string introduction,
				int experience,
				string imageUrl,
				int groupId);

		void Delete(int id);
	}


}
