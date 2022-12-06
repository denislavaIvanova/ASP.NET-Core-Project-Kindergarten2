

namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Models.Menus;
	using Microsoft.AspNetCore.Mvc;
	using System.Linq;

	public class MenusController : Controller
	{
		private readonly KindergartenDbContext data;

		public MenusController(KindergartenDbContext data)
			=> this.data = data;

		public IActionResult All([FromQuery] AllMenusQueryModel query)
		{
			var menusQuery = this.data.Menus.AsQueryable();

			if (!string.IsNullOrWhiteSpace(query.MenuType))
			{
				menusQuery = menusQuery.Where(m => m.MenuType == query.MenuType);
			}

			if (!string.IsNullOrWhiteSpace(query.SearchTerm))
			{
				menusQuery = menusQuery.Where(m => m.MenuType.ToLower().Contains(query.SearchTerm.ToLower()) ||
				  m.Description.ToLower().Contains(query.SearchTerm.ToLower()));
			}

			menusQuery = query.Sorting switch
			{
				MenuSorting.Price => menusQuery.OrderBy(m => m.Price),
				MenuSorting.DateCreated or _ => menusQuery.OrderByDescending(m => m.Id)
			};

			var totalMenus = menusQuery.Count();

			var menus = menusQuery
				.Skip((query.CurrentPage - 1) * AllMenusQueryModel.MenusPerPage)
				.Take(AllMenusQueryModel.MenusPerPage)
				.Select(m => new MenuListingViewModel
				{
					Id = m.Id,
					MenuType = m.MenuType,
					Price = m.Price,
					Description = m.Description,
					ImageUrl = m.ImageUrl

				}).ToList();

			var menusMenuType = this.data
				.Menus
				.Select(m => m.MenuType)
				.Distinct()
				.OrderBy(mt => mt)
				.ToList();

			query.Menus = menus;
			query.TotalMenus = totalMenus;
			query.MenuTypes = menusMenuType;

			return View(query);

		}


		public IActionResult Add() => View(new AddMenuFormModel
		{

		});

		[HttpPost]
		public IActionResult Add(AddMenuFormModel menu)
		{
			if (!ModelState.IsValid)
			{
				return View(menu);
			}

			var menuData = new Menu
			{
				MenuType = menu.MenuType,
				Description = menu.Description,
				Price = menu.Price,
				ImageUrl = menu.ImageUrl
			};

			this.data.Menus.Add(menuData);

			this.data.SaveChanges();

			return RedirectToAction("Index", "Home");
		}
	}
}
