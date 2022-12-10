
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

		IEnumerable<string> AllChildGroups();
	}
}
