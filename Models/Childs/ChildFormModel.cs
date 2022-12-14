namespace Kindergarten2.Services.Childs
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class ChildFormModel : IChildModel
	{

		[Required]
		[StringLength(100, MinimumLength = 1, ErrorMessage = "{0} must be between {2} and {1} symbols.")]
		[Display(Name = "First name")]
		public string FirstName { get; init; }

		[Required]
		[StringLength(100, MinimumLength = 1, ErrorMessage = "{0} must be between {2} and {1} symbols.")]
		[Display(Name = "Middle name")]
		public string MiddleName { get; init; }

		[Required]
		[StringLength(100, MinimumLength = 1, ErrorMessage = "{0} must be between {2} and {1} symbols.")]
		[Display(Name = "Last name")]
		public string LastName { get; init; }

		[Range(1, 6, ErrorMessage = "{0} must be between {1} and {2} years inclusive.")]
		public int Age { get; init; }



		[Display(Name = "Group")]
		public int GroupId { get; init; }

		public IEnumerable<ChildGroupServiceModel> Groups { get; set; }

		[Display(Name = "Trip")]
		public int TripId { get; init; }

		public IEnumerable<ChildTripServiceModels> Trips { get; set; }

		[Display(Name = "Menu")]
		public int MenuId { get; init; }

		public IEnumerable<ChildMenuServiceModel> Menus { get; set; }

		[Display(Name = "Extracurricular activity")]
		public int ECAId { get; init; }

		public IEnumerable<ChildECAServiceModel> ECAs { get; set; }
	}
}
