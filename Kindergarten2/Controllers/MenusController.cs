
namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Models.Menus;
	using Kindergarten2.Services.Menus;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using static Kindergarten2.Areas.Admin.AdminConstants;

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

		[Authorize(Roles = AdministratorRoleName)]


		public IActionResult Add() => View(new AddMenuFormModel
		{

		});

		[HttpPost]
		[Authorize(Roles = AdministratorRoleName)]


		public IActionResult Add(AddMenuFormModel menu)
		{
			if (!ModelState.IsValid)
			{
				return View(menu);
			}

			this.menus.Create(menu.MenuType,
				menu.Description,
				menu.Price,
				menu.ImageUrl);


			return RedirectToAction(nameof(All));

		}

		[Authorize(Roles = AdministratorRoleName)]
		public IActionResult Edit(int id)
		{
			var menu = this.menus.Details(id);

			return View(new MenuServiceModel
			{
				MenuType = menu.MenuType,
				Description = menu.Description,
				Price = menu.Price,
				ImageUrl = menu.ImageUrl
			});
		}

		[Authorize(Roles = AdministratorRoleName)]
		[HttpPost]

		public IActionResult Edit(int id, MenuServiceModel menu)
		{
			if (!ModelState.IsValid)
			{
				return View(menu);
			}

			this.menus.Edit(id,
				menu.MenuType,
				menu.Description,
				menu.Price,
				menu.ImageUrl);

			return RedirectToAction(nameof(All));
		}

		[Authorize]
		public IActionResult Details(int id)
		{
			var menu = this.menus.Details(id);

			return View(menu);

		}

		[Authorize(Roles = AdministratorRoleName)]
		[HttpPost]
		[HttpDelete]
		public IActionResult Delete(int id)
		{

			this.menus.Delete(id);
			return RedirectToAction(nameof(All));
		}
	}
}
