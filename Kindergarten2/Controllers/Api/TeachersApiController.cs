
namespace Kindergarten2.Controllers.Api
{
	using Kindergarten2.Models.Api.Teachers;
	using Kindergarten2.Services.Teachers;
	using Microsoft.AspNetCore.Mvc;

	[ApiController]
	[Route("api/teachers")]
	public class TeachersApiController : ControllerBase
	{
		private readonly ITeacherService teachers;

		public TeachersApiController(ITeacherService teachers)
			=> this.teachers = teachers;

		[HttpGet]
		public ActionResult<TeacherQueryServiceModel> All([FromQuery] AllTeachersApiRequestModel query)
			=> this.teachers.All(
				query.Specialization,
				query.SearchTerm,
				query.Sorting,
				query.CurrentPage,
				query.TeachersPerPage);

	}
}
