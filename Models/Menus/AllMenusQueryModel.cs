using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kindergarten2.Models.Menus
{
	public class AllMenusQueryModel
	{
		public const int MenusPerPage = 3;

		[Display(Name = "Menu Type")]
		public string MenuType { get; init; }

		public IEnumerable<string> MenuTypes { get; set; }

		[Display(Name = "Search")]
		public string SearchTerm { get; init; }

		public MenuSorting Sorting { get; init; }
		public int CurrentPage { get; init; } = 1;

		public int TotalMenus { get; set; }

		public IEnumerable<MenuListingViewModel> Menus { get; set; }
	}
}
