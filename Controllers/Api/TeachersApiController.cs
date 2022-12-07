
namespace Kindergarten2.Controllers.Api
{
	using Kindergarten2.Data;
	using Kindergarten2.Models.Api.Teachers;
	using Kindergarten2.Models.Teachers;
	using Microsoft.AspNetCore.Mvc;
	using System.Linq;

	[ApiController]
	[Route("api/teachers")]
	public class TeachersApiController : ControllerBase
	{
		private readonly KindergartenDbContext data;

		public TeachersApiController(KindergartenDbContext data)
				=> this.data = data;

		[HttpGet]
		public ActionResult<AllTeachersApiResponseModel> All([FromQuery] AllTeachersApiRequestModel query)
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
				.Skip((query.CurrentPage - 1) * query.TeachersPerPage)
				.Take(query.TeachersPerPage)
				.Select(t => new TeacherResponseModel
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

			return new AllTeachersApiResponseModel
			{
				CurrentPage = query.CurrentPage,
				TeachersPerPage = query.TeachersPerPage,
				TotalTeachers = totalTeachers,
				Teachers = teachers
			};

		}




	}
}
