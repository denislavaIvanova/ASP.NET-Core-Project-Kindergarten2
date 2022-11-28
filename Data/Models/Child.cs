namespace Kindergarten2.Data.Models
{
	using System.ComponentModel.DataAnnotations;

	public class Child
	{
		public int Id { get; init; }

		[Required]
		[MaxLength(100)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(100)]
		public string MiddleName { get; set; }

		[Required]
		[MaxLength(100)]
		public string LastName { get; set; }

		[Range(1, 6)]
		public int Age { get; set; }

		public int GroupId { get; set; }

		public Group Group { get; init; }

		public int TripId { get; set; }

		public Trip Trip { get; init; }

		public int MenuId { get; set; }

		public Menu Menu { get; init; }

		public int ECAId { get; set; }

		public ECA ECA { get; init; }
	}
}
