

namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Models.Menus;
	using Kindergarten2.Services.Menus;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	public class MenusController : Controller
	{
		private readonly KindergartenDbContext data;
		private readonly IMenuService menus;

		public MenusController(KindergartenDbContext data, IMenuService menus)
		{
			this.data = data;
			this.menus = menus;
		}

		public IActionResult All([FromQuery] AllMenusQueryModel query)
		{
			var queryResult = this.menus.All(query.MenuType,
				query.SearchTerm,
				query.Sorting,
				query.CurrentPage,
				AllMenusQueryModel.MenusPerPage);

			var menusMenuType = this.menus.AllMenuTypes();


			query.Menus = queryResult.Menus;
			query.TotalMenus = queryResult.TotalMenus;
			query.MenuTypes = menusMenuType;

			return View(query);

		}

		[Authorize]

		public IActionResult Add() => View(new AddMenuFormModel
		{

		});

		[HttpPost]
		[Authorize]

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

			return RedirectToAction(nameof(All));

		}
	}
}
