
namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Infrastructure;
	using Kindergarten2.Models.Parents;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using System.Linq;

	public class ParentsController : Controller
	{
		private readonly KindergartenDbContext data;

		public ParentsController(KindergartenDbContext data)
			=> this.data = data;

		[Authorize]
		public IActionResult Become() => View();

		[HttpPost]
		[Authorize]

		public IActionResult Become(RegisterAsParentFormModel parent)
		{
			var userId = this.User.Id();

			var userIsAlresdyParent = this.data
				.Parents.Any(p => p.UserId == userId);

			if (userIsAlresdyParent)
			{
				return BadRequest();
			}

			if (!ModelState.IsValid)
			{
				return View(parent);
			}

			var parentData = new Parent
			{
				FirstName = parent.FirstName,
				LastName = parent.LastName,
				PhoneNumber = parent.PhoneNumber,
				UserId = userId
			};

			this.data.Parents.Add(parentData);
			this.data.SaveChanges();

			return RedirectToAction("Add", "Childs");
		}
	}
}
