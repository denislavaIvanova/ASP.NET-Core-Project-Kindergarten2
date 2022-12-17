
namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Models.Teachers;
	using Kindergarten2.Services.Teachers;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using static Kindergarten2.Areas.Admin.AdminConstants;

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

		[Authorize(Roles = AdministratorRoleName)]

		//if you want to find additional info for the user var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;=>this.User.GetId()
		public IActionResult Add() => View(new TeacherFormModel
		{
			Groups = this.teachers.GetTeacherGroups()
		});

		[Authorize(Roles = AdministratorRoleName)]


		[HttpPost]
		public IActionResult Add(TeacherFormModel teacher)
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

		[Authorize(Roles = AdministratorRoleName)]
		public IActionResult Edit(int id)
		{
			var teacher = this.teachers.Details(id);

			return View(new TeacherFormModel
			{
				FirstName = teacher.FirstName,
				LastName = teacher.LastName,
				Specialization = teacher.Specialization,
				Introduction = teacher.Introduction,
				Experience = teacher.Experience,
				ImageUrl = teacher.ImageUrl,
				GroupId = teacher.GroupId,
				Groups = this.teachers.GetTeacherGroups()
			});
		}

		[Authorize(Roles = AdministratorRoleName)]
		[HttpPost]

		public IActionResult Edit(int id, TeacherFormModel teacher)
		{
			if (!this.teachers.GroupExist(teacher.GroupId))
			{
				this.ModelState.AddModelError(nameof(teacher.GroupId), "Group does not exist!");
			}

			if (!ModelState.IsValid)
			{
				teacher.Groups = this.teachers.GetTeacherGroups();
				return View(teacher);
			}

			this.teachers.Edit(id,
				teacher.FirstName,
				teacher.LastName,
				teacher.Specialization,
				teacher.Introduction,
				teacher.Experience,
				teacher.ImageUrl,
				teacher.GroupId);

			return RedirectToAction(nameof(All));

		}

		[Authorize]
		public IActionResult Details(int id)
		{
			var teacher = this.teachers.Details(id);

			return View(teacher);

		}

		[Authorize(Roles = AdministratorRoleName)]
		[HttpPost]
		[HttpDelete]
		public IActionResult Delete(int id)
		{

			this.teachers.Delete(id);
			return RedirectToAction(nameof(All));
		}

	}
}

