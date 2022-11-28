namespace Kindergarten2.Data.Models
{
	using System.ComponentModel.DataAnnotations;

	public class Teacher
	{
		public int Id { get; init; }

		public int GroupId { get; set; }

		public Group Group { get; init; }

		[Required]
		[MaxLength(100)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(100)]
		public string LastName { get; set; }

		public int Experience { get; set; }

		[Required]
		[MaxLength(300)]
		public string Specialization { get; set; }
	}
}
