

namespace Kindergarten2.Controllers
{
	using Kindergarten2.Models.Teachers;
	using Microsoft.AspNetCore.Mvc;
	public class TeachersController : Controller
	{
		public IActionResult Add() => View();

		[HttpPost]

		public IActionResult Add(AddTeacherFormModel teacher)
		{
			return View();
		}
	}
}

