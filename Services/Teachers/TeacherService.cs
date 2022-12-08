
namespace Kindergarten2.Services.Teachers
{
	using Kindergarten2.Data;
	using Kindergarten2.Models.Teachers;
	using System.Collections.Generic;
	using System.Linq;

	public class TeacherService : ITeacherService
	{
		private readonly KindergartenDbContext data;

		public TeacherService(KindergartenDbContext data)
			=> this.data = data;


		public TeacherQueryServiceModel All(
			string specialization,
			string searchTerm,
			TeacherSorting sorting,
			int currentPage,
			int teachersPerPage)
		{
			var teachersQuery = this.data.Teachers.AsQueryable();

			if (!string.IsNullOrWhiteSpace(specialization))
			{
				teachersQuery = teachersQuery.Where(t => t.Specialization == specialization);

			}

			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
				teachersQuery = teachersQuery.Where(t =>
				  (t.FirstName + " " + t.LastName).ToLower().Contains(searchTerm.ToLower()) ||
				  t.Introduction.ToLower().Contains(searchTerm.ToLower()));
			}

			teachersQuery = sorting switch
			{
				TeacherSorting.Experience => teachersQuery.OrderByDescending(t => t.Experience),
				TeacherSorting.FirstNameAndLastName => teachersQuery.OrderBy(t => t.FirstName).ThenBy(t => t.LastName),
				TeacherSorting.DateCreated or _ => teachersQuery.OrderByDescending(t => t.Id)
			};

			var totalTeachers = teachersQuery.Count();

			var teachers = teachersQuery
				.Skip((currentPage - 1) * teachersPerPage)
				.Take(teachersPerPage)
				.Select(t => new TeacherServiceModel
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

			return new TeacherQueryServiceModel
			{
				TotalTeachers = totalTeachers,
				Teachers = teachers,
				TeachersPerPage = teachersPerPage,
				CurrentPage = currentPage

			};
		}

		public IEnumerable<string> AllTeacherSpecializations()
				=> this.data
						.Teachers
						.Select(t => t.Specialization)
						.Distinct()
						.OrderBy(spec => spec)
						.ToList();
	}
}
