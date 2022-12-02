

namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Models.Trips;
	using Microsoft.AspNetCore.Mvc;
	public class TripsController : Controller
	{
		private readonly KindergartenDbContext data;

		public TripsController(KindergartenDbContext data)
			=> this.data = data;

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
