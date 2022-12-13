

namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
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

		[Authorize(Roles = "Administrator")]

		public IActionResult Add() => View(new AddTripFormModel
		{

		});

		[HttpPost]
		[Authorize(Roles = "Administrator")]


		public IActionResult Add(AddTripFormModel trip)
		{
			if (!ModelState.IsValid)
			{
				return View(trip);
			}

			this.trips.Craete(trip.PlaceToVisit,
				trip.Activity,
				trip.Location,
				trip.Price,
				trip.ImageUrl);

			return RedirectToAction(nameof(All));

		}

		[Authorize(Roles = "Administrator")]

		public IActionResult Edit(int id)
		{
			var trip = this.trips.Details(id);

			return View(new TripServiceModel
			{
				PlaceToVisit = trip.PlaceToVisit,
				Activity = trip.Activity,
				Location = trip.Location,
				ImageUrl = trip.ImageUrl,
				Price = trip.Price
			});

		}

		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public IActionResult Edit(int id, TripServiceModel trip)
		{
			if (!ModelState.IsValid)
			{
				return View(trip);
			}

			this.trips.Edit(id,
				trip.PlaceToVisit,
				trip.Activity,
				trip.Location,
				trip.Price,
				trip.ImageUrl);

			return RedirectToAction(nameof(All));

		}



	}
}
