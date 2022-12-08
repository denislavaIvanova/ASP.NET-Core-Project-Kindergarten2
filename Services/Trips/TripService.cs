

namespace Kindergarten2.Services.Trips
{
	using Kindergarten2.Data;
	using Kindergarten2.Models.Trips;
	using System.Collections.Generic;
	using System.Linq;
	public class TripService : ITripService
	{
		private readonly KindergartenDbContext data;

		public TripService(KindergartenDbContext data)
			=> this.data = data;

		public TripQueryServiceModel All(
			string placeToVisit,
			string searchTerm,
			TripSorting sorting,
			int currentPage,
			int tripsPerPage)
		{
			var tripsQuery = this.data.Trips.AsQueryable();

			if (!string.IsNullOrWhiteSpace(placeToVisit))
			{
				tripsQuery = tripsQuery.Where(t => t.PlaceToVisit == placeToVisit);
			}

			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
				tripsQuery = tripsQuery.Where(t => t.PlaceToVisit.ToLower().Contains(searchTerm.ToLower())
				  || t.Activity.ToLower().Contains(searchTerm.ToLower()));
			}

			tripsQuery = sorting switch
			{
				TripSorting.Location => tripsQuery.OrderBy(t => t.Location),
				TripSorting.Price => tripsQuery.OrderBy(t => t.Price),
				TripSorting.DateCreated or _ => tripsQuery.OrderByDescending(t => t.Id)

			};

			var totalTrips = tripsQuery.Count();

			var trips = tripsQuery
				.Skip((currentPage - 1) * tripsPerPage)
				.Take(tripsPerPage)
				.Select(t => new TripServiceModel
				{
					Id = t.Id,
					PlaceToVisit = t.PlaceToVisit,
					Activity = t.Activity,
					ImageUrl = t.ImageUrl,
					Price = t.Price,
					Location = t.Location
				}).ToList();

			return new TripQueryServiceModel
			{
				TotalTrips = totalTrips,
				TripsPerPage = tripsPerPage,
				CurrentPage = currentPage,
				Trips = trips

			};
		}

		public IEnumerable<string> AllTripPlacesToVisit()
			=> this.data
				.Trips
				.Select(t => t.PlaceToVisit)
				.Distinct()
				.OrderBy(p => p)
				.ToList();
	}
}
