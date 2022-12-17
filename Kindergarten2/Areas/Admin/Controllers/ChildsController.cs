

namespace Kindergarten2.Areas.Admin.Controllers
{

	using Kindergarten2.Services.Childs;
	using Microsoft.AspNetCore.Mvc;

	public class ChildsController : AdminController
	{
		private readonly IChildService children;

		public ChildsController(IChildService children)
		{
			this.children = children;
		}

		public IActionResult All()
		{
			var children = this.children.All(confirmedOnly: false).Children;

			return View(children);

		}

		public IActionResult ChangeVisibility(int id)
		{
			this.children.ChangeVisibility(id);

			return RedirectToAction(nameof(All));
		}
	}
}
