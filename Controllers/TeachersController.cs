namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Models.Teachers;
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using System.Linq;

	public class TeachersController : Controller
	{
		private readonly KindergartenDbContext data;

		public TeachersController(KindergartenDbContext data)
			=> this.data = data;
		public IActionResult Add() => View(new AddTeacherFormModel
		{
			Groups = this.GetTeacherGroups()
		});

		[HttpPost]
		public IActionResult Add(AddTeacherFormModel teacher)
		{
			if (!ModelState.IsValid)
			{
				teacher.Groups = this.GetTeacherGroups();

				return View(teacher);
			}

			return RedirectToAction("Index", "Home");
		}

		private IEnumerable<TeacherGroupViewModel> GetTeacherGroups()
				=> this.data
				.Groups
				.Select(t => new TeacherGroupViewModel
				{
					Id = t.Id,
					Name = t.Name,

				}).ToList();
	}
}

