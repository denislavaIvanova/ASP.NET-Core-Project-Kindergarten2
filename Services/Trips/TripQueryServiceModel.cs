
namespace Kindergarten2.Services.Trips
{
	using System.Collections.Generic;

	public class TripQueryServiceModel
	{
		public int CurrentPage { get; init; }

		public int TripsPerPage { get; init; }

		public int TotalTrips { get; init; }

		public IEnumerable<TripServiceModel> Trips { get; init; }
	}
}
