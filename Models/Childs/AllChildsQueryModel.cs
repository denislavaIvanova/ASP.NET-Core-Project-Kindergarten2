
namespace Kindergarten2.Models.Childs
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	public class AllChildsQueryModel
	{
		public const int ChildrenPerPage = 3;
		public string Group { get; init; }
		public IEnumerable<string> Groups { get; set; }

		[Display(Name = "Search")]
		public string SearchTerm { get; init; }

		public ChildSorting Sorting { get; init; }

		//current page if IT HAS NO VALUE, IT WILL START FROM 1
		public int CurrentPage { get; init; } = 1;

		public int TotalChildren { get; set; }
		public IEnumerable<ChildListingViewModel> Children { get; set; }
	}
}
