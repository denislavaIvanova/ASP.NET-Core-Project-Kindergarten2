
namespace Kindergarten2.Test.Controllers
{
	using Kindergarten2.Controllers;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Models.Trips;
	using Kindergarten2.Services.Trips;
	using Kindergarten2.Test.Mocks;
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using System.Linq;
	using Xunit;
	public class TripsControllerTests
	{
		[Fact]
		public void AllShouldReturnViewWithCorrectModel()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var trips = new List<TripServiceModel>();

			var tripFirst = new Trip
			{
				PlaceToVisit = "Vatican museum",
				Activity = "Sightseeing",
				Id = 1,
				ImageUrl = "someImageUrl",
				Location = "Vatican city",
				Price = 98
			};

			var tripSecond = new Trip
			{
				PlaceToVisit = "British museum",
				Activity = "Sightseeing",
				Id = 2,
				ImageUrl = "someImageUrl",
				Location = "London",
				Price = 898
			};

			trips.Add(new TripServiceModel
			{
				PlaceToVisit = "Vatican museum",
				Activity = "Sightseeing",
				Id = 1,
				ImageUrl = "someImageUrl",
				Location = "Vatican city",
				Price = 98
			});

			trips.Add(new TripServiceModel
			{
				PlaceToVisit = "British museum",
				Activity = "Sightseeing",
				Id = 2,
				ImageUrl = "someImageUrl",
				Location = "London",
				Price = 898
			});


			data.Trips.Add(tripFirst);
			data.Trips.Add(tripSecond);



			data.SaveChanges();

			var TripService = new TripService(data);

			var TripsController = new TripsController(data, TripService);

			//Act

			var result = TripsController.All(new AllTripsQueryModel
			{
				CurrentPage = 1,
				TotalTrips = 1,
				SearchTerm = "British museum",
				PlaceToVisit = "British museum",
				Sorting = default,
				Trips = trips
			});

			//Assert

			Assert.NotNull(result);

			var viewResult = Assert.IsType<ViewResult>(result);

			var model = viewResult.Model;

			var AllTripsQueryModel = Assert.IsType<AllTripsQueryModel>(model);

			Assert.NotNull(AllTripsQueryModel.SearchTerm);
			Assert.NotEqual(" ", AllTripsQueryModel.SearchTerm);
			Assert.Equal("British museum", AllTripsQueryModel.SearchTerm);

			Assert.NotNull(AllTripsQueryModel.PlaceToVisit);
			Assert.NotEqual(" ", AllTripsQueryModel.PlaceToVisit);
			Assert.Equal("British museum", AllTripsQueryModel.PlaceToVisit);

			Assert.Equal(TripSorting.DateCreated, AllTripsQueryModel.Sorting);

			Assert.Equal(1, AllTripsQueryModel.TotalTrips);
			Assert.Equal(1, AllTripsQueryModel.CurrentPage);
			Assert.Equal(3, AllTripsQueryModel.TripsPerPage);
			Assert.True(TripsController.ModelState.IsValid);

		}

		[Fact]
		public void GetAddShouldReturnViewWithCorrectModel()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var tripFirst = new Trip
			{
				PlaceToVisit = "Vatican museum",
				Activity = "Sightseeing",
				Id = 1,
				ImageUrl = "someImageUrl",
				Location = "Vatican city",
				Price = 98
			};

			var tripSecond = new Trip
			{
				PlaceToVisit = "British museum",
				Activity = "Sightseeing",
				Id = 2,
				ImageUrl = "someImageUrl",
				Location = "London",
				Price = 898
			};

			data.Trips.Add(tripFirst);
			data.Trips.Add(tripSecond);
			data.SaveChanges();

			var TripsService = new TripService(data);

			var TripsContoller = new TripsController(data, TripsService);


			//Act

			var result = TripsContoller.Add();

			//Assert

			Assert.NotNull(result);
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = viewResult.Model;
			var addTripFormModel = Assert.IsType<AddTripFormModel>(model);

		}

		[Fact]
		public void PostAddShouldReturnViewWithCorrectRedirectToAction()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var TripsService = new TripService(data);

			var TripsContoller = new TripsController(data, TripsService);

			//Act
			var newTrip = new AddTripFormModel
			{
				PlaceToVisit = "British museum",
				Activity = "Sightseeing",
				ImageUrl = "someImageUrl",
				Location = "London",
				Price = 898
			};

			var result = TripsContoller.Add(newTrip);

			var TripToTest = data.Trips.FirstOrDefault(e => e.Activity == newTrip.Activity);

			//Assert

			Assert.NotNull(result);
			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);


			Assert.Equal("All", redirectToActionResult.ActionName);

			Assert.True(TripsContoller.ModelState.IsValid);
			Assert.Null(redirectToActionResult.ControllerName);
			Assert.Equal(newTrip.PlaceToVisit, TripToTest.PlaceToVisit);
			Assert.Equal(newTrip.Activity, TripToTest.Activity);
			Assert.Equal(newTrip.Location, TripToTest.Location);
			Assert.Equal(newTrip.Price, TripToTest.Price);
			Assert.Equal(newTrip.ImageUrl, TripToTest.ImageUrl);

		}

		[Fact]
		public void GetEditShouldReturnViewWithCorrectModel()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var tripFirst = new Trip
			{
				PlaceToVisit = "Vatican museum",
				Activity = "Sightseeing",
				Id = 1,
				ImageUrl = "someImageUrl",
				Location = "Vatican city",
				Price = 98
			};

			var tripSecond = new Trip
			{
				PlaceToVisit = "British museum",
				Activity = "Sightseeing",
				Id = 2,
				ImageUrl = "someImageUrl",
				Location = "London",
				Price = 898
			};

			data.Trips.Add(tripFirst);
			data.Trips.Add(tripSecond);
			data.SaveChanges();

			var TripsService = new TripService(data);

			var TripsContoller = new TripsController(data, TripsService);


			//Act

			var result = TripsContoller.Edit(tripSecond.Id);

			//Assert

			Assert.NotNull(result);
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = viewResult.Model;
			var TripServiceModel = Assert.IsType<TripServiceModel>(model);
			Assert.Equal(tripSecond.PlaceToVisit, TripServiceModel.PlaceToVisit);
			Assert.Equal(tripSecond.Location, TripServiceModel.Location);
			Assert.Equal(tripSecond.Price, TripServiceModel.Price);
			Assert.Equal(tripSecond.Activity, TripServiceModel.Activity);
			Assert.Equal(tripSecond.ImageUrl, TripServiceModel.ImageUrl);

		}

		[Fact]
		public void PostEditShouldReturnViewWithCorrectRedirectToAction()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var TripsService = new TripService(data);

			var TripsContoller = new TripsController(data, TripsService);

			var tripFirst = new Trip
			{
				PlaceToVisit = "Vatican museum",
				Activity = "Sightseeing",
				Id = 1,
				ImageUrl = "someImageUrl",
				Location = "Vatican city",
				Price = 98
			};

			var tripSecond = new Trip
			{
				PlaceToVisit = "British museum",
				Activity = "Sightseeing",
				Id = 2,
				ImageUrl = "someImageUrl",
				Location = "London",
				Price = 898
			};

			data.Trips.Add(tripFirst);
			data.Trips.Add(tripSecond);
			data.SaveChanges();

			//Act
			var editedTrip = new TripServiceModel
			{
				PlaceToVisit = "Varna museum",
				Activity = "Sightseeing",
				Id = 2,
				ImageUrl = "someImageUrl",
				Location = "Varna",
				Price = 8
			};

			var result = TripsContoller.Edit(tripSecond.Id, editedTrip);

			var tripToTest = data.Trips.FirstOrDefault(e => e.Id == editedTrip.Id);

			//Assert

			Assert.NotNull(result);
			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);


			Assert.Equal("All", redirectToActionResult.ActionName);

			Assert.True(TripsContoller.ModelState.IsValid);
			Assert.Null(redirectToActionResult.ControllerName);
			Assert.Equal(editedTrip.PlaceToVisit, tripToTest.PlaceToVisit);
			Assert.Equal(editedTrip.Activity, tripToTest.Activity);
			Assert.Equal(editedTrip.ImageUrl, tripToTest.ImageUrl);
			Assert.Equal(editedTrip.Price, tripToTest.Price);
			Assert.Equal(editedTrip.Location, tripToTest.Location);

		}

		//[Fact]
		//public void PostDeleteShouldRedirectToAllAndRemoveEntryFromDb()
		//{
		//	//Arrange
		//	var data = DatabaseMock.Instance;

		//	var TripsService = new TripService(data);

		//	var TripsContoller = new TripsController(data, TripsService);

		//	var tripFirst = new Trip
		//	{
		//		PlaceToVisit = "Vatican museum",
		//		Activity = "Sightseeing",
		//		Id = 1,
		//		ImageUrl = "someImageUrl",
		//		Location = "Vatican city",
		//		Price = 98
		//	};

		//	var tripSecond = new Trip
		//	{
		//		PlaceToVisit = "British museum",
		//		Activity = "Sightseeing",
		//		Id = 2,
		//		ImageUrl = "someImageUrl",
		//		Location = "London",
		//		Price = 898
		//	};

		//	data.Trips.Add(tripFirst);
		//	data.Trips.Add(tripSecond);
		//	data.SaveChanges();

		//	//Act
		//	var result = TripsContoller.Delete(tripFirst.Id);

		//	var tripToTest = data.Trips.FirstOrDefault(t => t.Id == tripFirst.Id);

		//	//Assert

		//	Assert.NotNull(result);
		//	var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);


		//	Assert.Equal("All", redirectToActionResult.ActionName);

		//	Assert.Null(redirectToActionResult.ControllerName);
		//	Assert.Null(tripToTest);

		//}

		[Fact]
		public void GetDetailsShouldReturnViewWithCorrectModel()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var tripFirst = new Trip
			{
				PlaceToVisit = "Vatican museum",
				Activity = "Sightseeing",
				Id = 1,
				ImageUrl = "someImageUrl",
				Location = "Vatican city",
				Price = 98
			};

			var tripSecond = new Trip
			{
				PlaceToVisit = "British museum",
				Activity = "Sightseeing",
				Id = 2,
				ImageUrl = "someImageUrl",
				Location = "London",
				Price = 898
			};

			data.Trips.Add(tripFirst);
			data.Trips.Add(tripSecond);
			data.SaveChanges();

			var TripsService = new TripService(data);

			var TripsContoller = new TripsController(data, TripsService);


			//Act

			var result = TripsContoller.Details(tripSecond.Id);

			//Assert

			Assert.NotNull(result);
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = viewResult.Model;
			var TripServiceModel = Assert.IsType<TripServiceModel>(model);
			Assert.Equal(tripSecond.PlaceToVisit, TripServiceModel.PlaceToVisit);
			Assert.Equal(tripSecond.Location, TripServiceModel.Location);
			Assert.Equal(tripSecond.Price, TripServiceModel.Price);
			Assert.Equal(tripSecond.Activity, TripServiceModel.Activity);
			Assert.Equal(tripSecond.ImageUrl, TripServiceModel.ImageUrl);

		}


	}
}

