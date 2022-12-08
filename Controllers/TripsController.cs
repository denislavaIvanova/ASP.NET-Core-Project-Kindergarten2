

namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Models.Trips;
	using Kindergarten2.Services.Trips;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	public class TripsController : Controller
	{
		private readonly KindergartenDbContext data;

		private readonly ITripService trips;

		public TripsController(KindergartenDbContext data, ITripService trips)
		{
			this.trips = trips;
			this.data = data;
		}

		public IActionResult All([FromQuery] AllTripsQueryModel query)
		{
			var queryResult = this.trips.All
				(query.PlaceToVisit,
				query.SearchTerm,
				query.Sorting,
				query.CurrentPage,
				AllTripsQueryModel.TripsPerPage);

			var tripPlacesToVisit = this.trips.AllTripPlacesToVisit();

			query.Trips = queryResult.Trips;
			query.PlacesToVisit = tripPlacesToVisit;
			query.TotalTrips = queryResult.TotalTrips;

			return View(query);

		}

		[Authorize]
		public IActionResult Add() => View(new AddTripFormModel
		{

		});

		[HttpPost]
		[Authorize]

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

			return RedirectToAction(nameof(All));

		}

	}
}
