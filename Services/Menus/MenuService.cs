namespace Kindergarten2.Services.Menus
{
	using Kindergarten2.Data;
	using Kindergarten2.Data.Models;
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

		public int Create(string menuType, string description, double price, string imageUrl)
		{
			var menuData = new Menu
			{
				MenuType = menuType,
				Description = description,
				Price = price,
				ImageUrl = imageUrl
			};

			this.data.Menus.Add(menuData);

			this.data.SaveChanges();

			return menuData.Id;
		}

		public MenuServiceModel Details(int id)
			=> this.data
			.Menus.Where(m => m.Id == id)
			.Select(m => new MenuServiceModel
			{
				Id = m.Id,
				MenuType = m.MenuType,
				Description = m.Description,
				Price = m.Price,
				ImageUrl = m.ImageUrl
			}).FirstOrDefault();

		public bool Edit(int id, string menuType, string description, double price, string imageUrl)
		{
			var menuData = this.data.Menus.Find(id);

			if (menuData == null)
			{
				return false;
			}

			menuData.MenuType = menuType;
			menuData.Description = description;
			menuData.Price = price;
			menuData.ImageUrl = imageUrl;

			this.data.SaveChanges();

			return true;
		}

		public void Delete(int id)
		{
			var menuToDelete = this.data.Menus.Find(id);

			this.data.Menus.Remove(menuToDelete);

			this.data.SaveChanges();
		}
	}
}
