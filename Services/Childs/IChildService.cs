
namespace Kindergarten2.Services.Childs
{
	using Kindergarten2.Models.Childs;
	using System.Collections.Generic;

	public interface IChildService
	{
		ChildQueryServiceModel All(
			string group,
			string searchTerm,
			ChildSorting sorting,
			int currentPage,
			int childrenPerPage);

		int Create(string firstName,
				string middleName,
				string lastName,
				int age,
				int ECAId,
				int menuId,
				int groupId,
				int tripId,
				int parentId);

		bool Edit(int childId,
				string firstName,
				string middleName,
				string lastName,
				int age,
				int ECAId,
				int menuId,
				int groupId,
				int tripId);

		ChildDetailsServiceModel Deatails(int childId);
		IEnumerable<ChildServiceModel> ByUser(string userId);

		bool ChildIsByParent(int childId, int parentId);

		IEnumerable<string> AllChildGroups();

		IEnumerable<ChildGroupServiceModel> GetChildGroups();

		IEnumerable<ChildECAServiceModel> GetChildECAs();

		IEnumerable<ChildMenuServiceModel> GetChildMenus();

		IEnumerable<ChildTripServiceModels> GetChildTrips();

		bool GroupExist(int groupId);

		bool ECAExist(int ECAId);

		bool MenuExist(int menuId);

		bool TripExist(int tripId);

	}
}
