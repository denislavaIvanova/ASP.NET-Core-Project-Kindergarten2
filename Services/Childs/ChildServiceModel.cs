using System.ComponentModel.DataAnnotations;

namespace Kindergarten2.Services.Childs
{
	public class ChildServiceModel : IChildModel
	{
		public int Id { get; init; }

		public string FirstName { get; init; }

		public string MiddleName { get; init; }

		public string LastName { get; init; }

		public int Age { get; init; }

		[Display(Name = "Group Name")]
		public string GroupName { get; init; }

		[Display(Name = "Trip Name")]

		public string TripName { get; init; }

		[Display(Name = "Menu Name")]

		public string MenuName { get; init; }

		[Display(Name = "Activity Name")]

		public string ECAName { get; init; }
	}
}
