namespace Kindergarten2.Models.Api.Teachers
{
	public class TeacherResponseModel
	{
		public int Id { get; init; }
		public string FirstName { get; init; }

		public string LastName { get; init; }

		public int Experience { get; init; }

		public string Specialization { get; init; }

		public string Introduction { get; init; }

		public string ImageUrl { get; init; }

		public string Group { get; init; }
	}
}
