namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Models.Teachers;
	using Kindergarten2.Services.Teachers;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using System.Linq;

	public class TeachersController : Controller
	{
		private readonly KindergartenDbContext data;
		private readonly ITeacherService teachers;
		public TeachersController(KindergartenDbContext data, ITeacherService teachers)
		{
			this.data = data;
			this.teachers = teachers;
		}

		public IActionResult All([FromQuery] AllTeachersQueryModel query)
		{
			var queryResult = this.teachers.All(
					query.Specialization,
					query.SearchTerm,
					query.Sorting,
					query.CurrentPage,
					AllTeachersQueryModel.TeachersPerPage
				);

			var teacherSpecializations = teachers.AllTeacherSpecializations();

			query.TotalTeachers = queryResult.TotalTeachers;
			query.Specializations = teacherSpecializations;
			query.Teachers = queryResult.Teachers;

			return View(query);



		}

		[Authorize(Roles = "Administrator")]

		//if you want to find additional info for the user var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;=>this.User.GetId()
		public IActionResult Add() => View(new AddTeacherFormModel
		{
			Groups = this.GetTeacherGroups()
		});

		[Authorize(Roles = "Administrator")]


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
				Specialization = teacher.Specialization,
				Introduction = teacher.Introduction,
				ImageUrl = teacher.ImageUrl

			};

			this.data.Teachers.Add(teacherData);

			this.data.SaveChanges();

			return RedirectToAction(nameof(All));
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

