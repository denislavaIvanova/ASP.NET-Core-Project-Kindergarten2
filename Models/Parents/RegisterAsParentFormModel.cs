namespace Kindergarten2.Models.Parents
{
	using System.ComponentModel.DataAnnotations;
	public class RegisterAsParentFormModel
	{
		[Required]
		[StringLength(25, MinimumLength = 2)]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required]
		[StringLength(25, MinimumLength = 2)]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required]
		[StringLength(25, MinimumLength = 6)]
		[Display(Name = "Phone Number")]

		public string PhoneNumber { get; set; }
	}
}
