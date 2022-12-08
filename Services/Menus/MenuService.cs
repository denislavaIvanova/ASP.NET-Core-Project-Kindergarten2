namespace Kindergarten2.Services.Menus
{
	using Kindergarten2.Data;
	using Kindergarten2.Models.Menus;
	using System.Collections.Generic;
	using System.Linq;

	public class MenuService : IMenuService
	{
		private readonly KindergartenDbContext data;

		public MenuService(KindergartenDbContext data)
			=> this.data = data;
		public MenuQueryServiceModel All(string menuType, string searchTerm, MenuSorting sorting, int currentPage, int menusPerPage)
		{
			var menusQuery = this.data.Menus.AsQueryable();

			if (!string.IsNullOrWhiteSpace(menuType))
			{
				menusQuery = menusQuery.Where(m => m.MenuType == menuType);
			}

			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
				menusQuery = menusQuery.Where(m => m.MenuType.ToLower().Contains(searchTerm.ToLower()) ||
				  m.Description.ToLower().Contains(searchTerm.ToLower()));
			}

			menusQuery = sorting switch
			{
				MenuSorting.Price => menusQuery.OrderBy(m => m.Price),
				MenuSorting.DateCreated or _ => menusQuery.OrderByDescending(m => m.Id)
			};

			var totalMenus = menusQuery.Count();

			var menus = menusQuery
				.Skip((currentPage - 1) * menusPerPage)
				.Take(menusPerPage)
				.Select(m => new MenuServiceModel
				{
					Id = m.Id,
					MenuType = m.MenuType,
					Price = m.Price,
					Description = m.Description,
					ImageUrl = m.ImageUrl

				}).ToList();

			return new MenuQueryServiceModel
			{
				TotalMenus = totalMenus,
				MenusPerPage = menusPerPage,
				CurrentPage = currentPage,
				Menus = menus

			};
		}

		public IEnumerable<string> AllMenuTypes()
			=> this.data
				.Menus
				.Select(m => m.MenuType)
				.Distinct()
				.OrderBy(mt => mt)
				.ToList();
	}
}
