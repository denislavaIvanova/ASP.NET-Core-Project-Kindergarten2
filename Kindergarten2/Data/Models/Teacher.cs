namespace Kindergarten2.Data.Models
{
	using System.ComponentModel.DataAnnotations;

	public class Teacher
	{
		public int Id { get; init; }

		public int GroupId { get; set; }

		public Group Group { get; init; }

		[Required]
		[MinLength(2)]
		[MaxLength(100)]
		public string FirstName { get; set; }

		[Required]
		[MinLength(2)]
		[MaxLength(100)]
		public string LastName { get; set; }

		[Required]
		public string ImageUrl { get; set; }

		[Range(0, 100)]
		public int Experience { get; set; }

		[Required]
		[MinLength(2)]
		[MaxLength(300)]
		public string Specialization { get; set; }

		[Required]
		[MinLength(2)]
		[MaxLength(600)]
		public string Introduction { get; set; }


	}
}
