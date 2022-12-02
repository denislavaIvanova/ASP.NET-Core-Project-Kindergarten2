

namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Models.Menus;
	using Microsoft.AspNetCore.Mvc;
	public class MenusController : Controller
	{
		private readonly KindergartenDbContext data;

		public MenusController(KindergartenDbContext data)
			=> this.data = data;
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
