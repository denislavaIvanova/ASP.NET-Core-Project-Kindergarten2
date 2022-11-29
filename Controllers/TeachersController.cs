namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Data.Models;
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
			if (!this.data.Groups.Any(g => g.Id == teacher.GroupId))
			{
				this.ModelState.AddModelError(nameof(teacher.GroupId), "Group does not exist.");
			}

			if (!ModelState.IsValid)
			{
				teacher.Groups = this.GetTeacherGroups();

				return View(teacher);
			}

			var teacherData = new Teacher
			{
				GroupId = teacher.GroupId,
				FirstName = teacher.FirstName,
				LastName = teacher.LastName,
				Experience = teacher.Experience,
				Specialization = teacher.Specialization

			};

			this.data.Teachers.Add(teacherData);

			this.data.SaveChanges();

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

