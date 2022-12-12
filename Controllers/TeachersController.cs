namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Models.Teachers;
	using Kindergarten2.Services.Teachers;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

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
			Groups = this.teachers.GetTeacherGroups()
		});

		[Authorize(Roles = "Administrator")]


		[HttpPost]
		public IActionResult Add(AddTeacherFormModel teacher)
		{
			if (!this.teachers.GroupExist(teacher.GroupId))
			{
				this.ModelState.AddModelError(nameof(teacher.GroupId), "Group does not exist.");
			}

			if (!ModelState.IsValid)
			{
				teacher.Groups = this.teachers.GetTeacherGroups();

				return View(teacher);
			}

			this.teachers.Create(teacher.GroupId,
				teacher.FirstName,
				teacher.LastName,
				teacher.Experience,
				teacher.Specialization,
				teacher.Introduction,
				teacher.ImageUrl);

			return RedirectToAction(nameof(All));
		}

	}
}

