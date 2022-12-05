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

		public IActionResult All([FromQuery] AllTeachersQueryModel query)
		{
			var teachersQuery = this.data.Teachers.AsQueryable();

			if (!string.IsNullOrWhiteSpace(query.Specialization))
			{
				teachersQuery = teachersQuery.Where(t => t.Specialization == query.Specialization);

			}

			if (!string.IsNullOrWhiteSpace(query.SearchTerm))
			{
				teachersQuery = teachersQuery.Where(t =>
				  (t.FirstName + " " + t.LastName).ToLower().Contains(query.SearchTerm.ToLower()) ||
				  t.Introduction.ToLower().Contains(query.SearchTerm.ToLower()));
			}

			teachersQuery = query.Sorting switch
			{
				TeacherSorting.Experience => teachersQuery.OrderByDescending(t => t.Experience),
				TeacherSorting.FirstNameAndLastName => teachersQuery.OrderBy(t => t.FirstName).ThenBy(t => t.LastName),
				TeacherSorting.DateCreated or _ => teachersQuery.OrderByDescending(t => t.Id)
			};

			var totalTeachers = teachersQuery.Count();

			var teachers = teachersQuery
				.Skip((query.CurrentPage - 1) * AllTeachersQueryModel.TeachersPerPage)
				.Take(AllTeachersQueryModel.TeachersPerPage)
				.Select(t => new TeacherListingViewModel
				{
					Id = t.Id,
					FirstName = t.FirstName,
					LastName = t.LastName,
					Experience = t.Experience,
					Specialization = t.Specialization,
					Introduction = t.Introduction,
					Group = t.Group.Name,
					ImageUrl = t.ImageUrl
				})
				.ToList();

			var teacherSpecializations = this.data
						.Teachers
						.Select(t => t.Specialization)
						.Distinct()
						.OrderBy(spec => spec)
						.ToList();


			query.TotalTeachers = totalTeachers;
			query.Specializations = teacherSpecializations;
			query.Teachers = teachers;

			return View(query);

		}

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

