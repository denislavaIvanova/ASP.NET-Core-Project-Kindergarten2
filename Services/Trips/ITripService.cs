
namespace Kindergarten2.Services.Trips
{
	using Kindergarten2.Models.Trips;
	using System.Collections.Generic;

	public interface ITripService
	{
		TripQueryServiceModel All(string placeToVisit,
			string searchTerm,
			TripSorting sorting,
			int currentPage,
			int tripsPerPage);

		IEnumerable<string> AllTripPlacesToVisit();
	}
}
