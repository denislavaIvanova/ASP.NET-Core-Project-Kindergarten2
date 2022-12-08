
namespace Kindergarten2.Models.ECAs
{
	using Kindergarten2.Services.ECAs;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class AllECAsQueryModel
	{
		public const int ECAsPerPage = 3;
		public string Title { get; init; }
		public IEnumerable<string> Titles { get; set; }

		[Display(Name = "Search")]
		public string SearchTerm { get; init; }

		public ECASorting Sorting { get; init; }

		public int TotalECAs { get; set; }

		public int CurrentPage { get; init; } = 1;
		public IEnumerable<ECAServiceModel> ECAs { get; set; }
	}
}
