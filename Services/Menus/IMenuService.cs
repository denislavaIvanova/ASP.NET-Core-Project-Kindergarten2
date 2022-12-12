
namespace Kindergarten2.Services.Menus
{
	using Kindergarten2.Models.Menus;
	using System.Collections.Generic;

	public interface IMenuService
	{
		MenuQueryServiceModel All(string menuType,
			string searchTerm,
			MenuSorting sorting,
			int currentPage,
			int menusPerPage);

		IEnumerable<string> AllMenuTypes();

		int Create(string menuType, string description, double price, string imageUrl);
	}
}
