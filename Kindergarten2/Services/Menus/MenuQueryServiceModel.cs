
namespace Kindergarten2.Services.Menus
{
	using System.Collections.Generic;

	public class MenuQueryServiceModel
	{
		public int CurrentPage { get; init; }

		public int MenusPerPage { get; init; }

		public int TotalMenus { get; init; }

		public IEnumerable<MenuServiceModel> Menus { get; init; }
	}
}
