
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

		int Craete(string placeToVisit,
		string activity,
		string location,
		double price,
		string imageUrl);

		bool Edit(int id, string placeToVisit,
	string activity,
	string location,
	double price,
	string imageUrl);

		TripServiceModel Details(int id);

		void Delete(int id);
	}
}
