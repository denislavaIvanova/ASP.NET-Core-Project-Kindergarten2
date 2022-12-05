
namespace Kindergarten2.Models.Trips
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class AllTripsQueryModel
	{
		public const int TripsPerPage = 3;

		[Display(Name = "Place to Visit")]
		public string PlaceToVisit { get; init; }

		public IEnumerable<string> PlacesToVisit { get; set; }

		[Display(Name = "Search")]
		public string SearchTerm { get; init; }

		public TripSorting Sorting { get; init; }
		public int CurrentPage { get; init; } = 1;

		public int TotalTrips { get; set; }

		public IEnumerable<TripListingViewModel> Trips { get; set; }
	}
}

