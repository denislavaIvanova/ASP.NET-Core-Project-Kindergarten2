

namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Models.Trips;
	using Microsoft.AspNetCore.Mvc;
	using System.Linq;

	public class TripsController : Controller
	{
		private readonly KindergartenDbContext data;

		public TripsController(KindergartenDbContext data)
			=> this.data = data;

		public IActionResult All([FromQuery] AllTripsQueryModel query)
		{
			var tripsQuery = this.data.Trips.AsQueryable();

			if (!string.IsNullOrWhiteSpace(query.PlaceToVisit))
			{
				tripsQuery = tripsQuery.Where(t => t.PlaceToVisit == query.PlaceToVisit);
			}

			if (!string.IsNullOrWhiteSpace(query.SearchTerm))
			{
				tripsQuery = tripsQuery.Where(t => t.PlaceToVisit.ToLower().Contains(query.SearchTerm.ToLower())
				  || t.Activity.ToLower().Contains(query.SearchTerm.ToLower()));
			}

			tripsQuery = query.Sorting switch
			{
				TripSorting.Location => tripsQuery.OrderBy(t => t.Location),
				TripSorting.Price => tripsQuery.OrderBy(t => t.Price),
				TripSorting.DateCreated or _ => tripsQuery.OrderByDescending(t => t.Id)

			};

			var totalTrips = tripsQuery.Count();

			var trips = tripsQuery
				.Skip((query.CurrentPage - 1) * AllTripsQueryModel.TripsPerPage)
				.Take(AllTripsQueryModel.TripsPerPage)
				.Select(t => new TripListingViewModel
				{
					Id = t.Id,
					PlaceToVisit = t.PlaceToVisit,
					Activity = t.Activity,
					ImageUrl = t.ImageUrl,
					Price = t.Price,
					Location = t.Location
				}).ToList();

			var tripsPlacesTovisit = this.data
				.Trips
				.Select(t => t.PlaceToVisit)
				.Distinct()
				.OrderBy(p => p)
				.ToList();

			query.Trips = trips;
			query.PlacesToVisit = tripsPlacesTovisit;
			query.TotalTrips = totalTrips;

			return View(query);

		}
		public IActionResult Add() => View(new AddTripFormModel
		{

		});

		[HttpPost]

		public IActionResult Add(AddTripFormModel trip)
		{
			if (!ModelState.IsValid)
			{
				return View(trip);
			}

			var tripData = new Trip
			{
				PlaceToVisit = trip.PlaceToVisit,
				Activity = trip.Activity,
				Location = trip.Location,
				Price = trip.Price,
				ImageUrl = trip.ImageUrl
			};

			this.data.Trips.Add(tripData);

			this.data.SaveChanges();

			return RedirectToAction("Index", "Home");
		}

	}
}
