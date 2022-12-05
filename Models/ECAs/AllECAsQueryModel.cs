
namespace Kindergarten2.Models.ECAs
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class AllECAsQueryModel
	{
		public const int CarsPerPage = 3;
		public string Title { get; init; }
		public IEnumerable<string> Titles { get; set; }

		[Display(Name = "Search")]
		public string SearchTerm { get; init; }

		public ECASorting Sorting { get; init; }
		public IEnumerable<ECAListingViewModel> ECAs { get; set; }
	}
}
