
namespace Kindergarten2.Test.Controllers
{
	using Kindergarten2.Controllers;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Models.ECAs;
	using Kindergarten2.Services.ECAs;
	using Kindergarten2.Test.Mocks;
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using System.Linq;
	using Xunit;
	public class ECAsControllerTests
	{
		[Fact]
		public void AllShouldReturnViewWithCorrectModel()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var ECAs = new List<ECAServiceModel>();

			var ECAFirst = new ECA
			{
				Description = "Best swimming course",
				Title = "Swimming",
				Id = 1,
				ImageUrl = "some imageUrl",
				MonthlyFee = 9,


			};

			var ECASecond = new ECA
			{

				Description = "Best painting course",
				Title = "Painting",
				Id = 2,
				ImageUrl = "mageUrl",
				MonthlyFee = 8


			};
			ECAs.Add(new ECAServiceModel
			{
				Description = "Best swimming course",
				Title = "Swimming",
				Id = 1,
				ImageUrl = "some imageUrl",
				MonthlyFee = 9,
			});

			ECAs.Add(new ECAServiceModel
			{
				Description = "Best painting course",
				Title = "Painting",
				Id = 2,
				ImageUrl = "mageUrl",
				MonthlyFee = 8
			});


			data.ECAs.Add(ECAFirst);
			data.ECAs.Add(ECASecond);



			data.SaveChanges();

			var ECAsService = new ECAService(data);

			var ECAsContoller = new ECAsController(data, ECAsService);

			//Act



			var result = ECAsContoller.All(new AllECAsQueryModel
			{
				CurrentPage = 1,
				TotalECAs = 1,
				SearchTerm = "Swimming",
				Title = "Swimming",
				Sorting = default,
				ECAs = ECAs
			});

			//Assert

			Assert.NotNull(result);

			var viewResult = Assert.IsType<ViewResult>(result);

			var model = viewResult.Model;

			var AllECAsQueryModel = Assert.IsType<AllECAsQueryModel>(model);

			Assert.NotNull(AllECAsQueryModel.SearchTerm);
			Assert.NotEqual(" ", AllECAsQueryModel.SearchTerm);
			Assert.Equal("Swimming", AllECAsQueryModel.SearchTerm);

			Assert.NotNull(AllECAsQueryModel.Title);
			Assert.NotEqual(" ", AllECAsQueryModel.Title);
			Assert.Equal("Swimming", AllECAsQueryModel.Title);

			Assert.Equal(ECASorting.DateCreated, AllECAsQueryModel.Sorting);

			Assert.Equal(1, AllECAsQueryModel.TotalECAs);
			Assert.Equal(1, AllECAsQueryModel.CurrentPage);
			Assert.Equal(3, AllECAsQueryModel.ECAsPerPage);
			Assert.True(ECAsContoller.ModelState.IsValid);

		}

		[Fact]
		public void GetAddShouldReturnViewWithCorrectModel()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var ECAFirst = new ECA
			{
				Description = "Best swimming course",
				Title = "Swimming",
				Id = 1,
				ImageUrl = "some imageUrl",
				MonthlyFee = 9,
			};

			var ECASecond = new ECA
			{

				Description = "Best painting course",
				Title = "Painting",
				Id = 2,
				ImageUrl = "mageUrl",
				MonthlyFee = 8

			};

			data.ECAs.Add(ECAFirst);
			data.ECAs.Add(ECASecond);
			data.SaveChanges();

			var ECAsService = new ECAService(data);

			var ECAsContoller = new ECAsController(data, ECAsService);


			//Act

			var result = ECAsContoller.Add();

			//Assert

			Assert.NotNull(result);
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = viewResult.Model;
			var addECAFormModel = Assert.IsType<AddECAFormModel>(model);

		}

		[Fact]
		public void PostAddShouldReturnViewWithCorrectRedirectToAction()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var ECAsService = new ECAService(data);

			var ECAsContoller = new ECAsController(data, ECAsService);

			//Act
			var newECA = new AddECAFormModel
			{
				Description = "Best horse riding course",
				Title = "Horse Riding",
				ImageUrl = "imageUrl",
				MonthlyFee = 800,
			};

			var result = ECAsContoller.Add(newECA);

			var ECAToTest = data.ECAs.FirstOrDefault(e => e.Title == newECA.Title);

			//Assert

			Assert.NotNull(result);
			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);


			Assert.Equal("All", redirectToActionResult.ActionName);

			Assert.True(ECAsContoller.ModelState.IsValid);
			Assert.Null(redirectToActionResult.ControllerName);
			Assert.Equal(newECA.Description, ECAToTest.Description);
			Assert.Equal(newECA.Title, ECAToTest.Title);
			Assert.Equal(newECA.ImageUrl, ECAToTest.ImageUrl);
			Assert.Equal(newECA.MonthlyFee, ECAToTest.MonthlyFee);

		}


		[Fact]
		public void GetEditShouldReturnViewWithCorrectModel()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var ECAFirst = new ECA
			{
				Description = "Best swimming course",
				Title = "Swimming",
				Id = 1,
				ImageUrl = "some imageUrl",
				MonthlyFee = 9,
			};

			var ECASecond = new ECA
			{

				Description = "Best painting course",
				Title = "Painting",
				Id = 2,
				ImageUrl = "mageUrl",
				MonthlyFee = 8

			};

			data.ECAs.Add(ECAFirst);
			data.ECAs.Add(ECASecond);
			data.SaveChanges();

			var ECAsService = new ECAService(data);

			var ECAsContoller = new ECAsController(data, ECAsService);


			//Act

			var result = ECAsContoller.Edit(ECASecond.Id);

			//Assert

			Assert.NotNull(result);
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = viewResult.Model;
			var ECAServiceModel = Assert.IsType<ECAServiceModel>(model);
			Assert.Equal(ECASecond.Title, ECAServiceModel.Title);
			Assert.Equal(ECASecond.Description, ECAServiceModel.Description);
			Assert.Equal(ECASecond.MonthlyFee, ECAServiceModel.MonthlyFee);
			Assert.Equal(ECASecond.ImageUrl, ECAServiceModel.ImageUrl);

		}

		[Fact]
		public void PostEditShouldReturnViewWithCorrectRedirectToAction()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var ECAsService = new ECAService(data);

			var ECAsContoller = new ECAsController(data, ECAsService);

			var ECAFirst = new ECA
			{
				Description = "Best swimming course",
				Title = "Swimming",
				Id = 1,
				ImageUrl = "some imageUrl",
				MonthlyFee = 9,
			};

			var ECASecond = new ECA
			{

				Description = "Best painting course",
				Title = "Painting",
				Id = 2,
				ImageUrl = "mageUrl",
				MonthlyFee = 8

			};

			data.ECAs.Add(ECAFirst);
			data.ECAs.Add(ECASecond);
			data.SaveChanges();

			//Act
			var editedECA = new ECAServiceModel
			{
				Description = "Best horse riding course",
				Title = "Horse Riding",
				ImageUrl = "imageUrl",
				MonthlyFee = 800,
			};

			var result = ECAsContoller.Edit(ECAFirst.Id, editedECA);

			var ECAToTest = data.ECAs.FirstOrDefault(e => e.Title == editedECA.Title);

			//Assert

			Assert.NotNull(result);
			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);


			Assert.Equal("All", redirectToActionResult.ActionName);

			Assert.True(ECAsContoller.ModelState.IsValid);
			Assert.Null(redirectToActionResult.ControllerName);
			Assert.Equal(editedECA.Description, ECAToTest.Description);
			Assert.Equal(editedECA.Title, ECAToTest.Title);
			Assert.Equal(editedECA.ImageUrl, ECAToTest.ImageUrl);
			Assert.Equal(editedECA.MonthlyFee, ECAToTest.MonthlyFee);

		}

		//[Fact]
		//public void PostDeleteShouldRedirectToAllAndRemoveEntryFromDb()
		//{
		//	//Arrange
		//	var data = DatabaseMock.Instance;

		//	var ECAsService = new ECAService(data);

		//	var ECAsContoller = new ECAsController(data, ECAsService);

		//	var ECAFirst = new ECA
		//	{
		//		Description = "Best swimming course",
		//		Title = "Swimming",
		//		Id = 1,
		//		ImageUrl = "some imageUrl",
		//		MonthlyFee = 9,
		//	};

		//	var ECASecond = new ECA
		//	{

		//		Description = "Best painting course",
		//		Title = "Painting",
		//		Id = 2,
		//		ImageUrl = "mageUrl",
		//		MonthlyFee = 8

		//	};

		//	data.ECAs.Add(ECAFirst);
		//	data.ECAs.Add(ECASecond);
		//	data.SaveChanges();

		//	//Act

		//	var deletedECA = new ECAServiceModel
		//	{
		//		Description = "Best swimming course",
		//		Title = "Swimming",
		//		Id = 1,
		//		ImageUrl = "some imageUrl",
		//		MonthlyFee = 9,
		//	};
		//	var result = ECAsContoller.Delete(deletedECA.Id);

		//	var ECAToTest = data.ECAs.FirstOrDefault(e => e.Id == deletedECA.Id);

		//	//Assert

		//	Assert.NotNull(result);
		//	var viewResult = Assert.IsType<ViewResult>(result);
		//	Assert.Null(ECAToTest);

		//}

		[Fact]
		public void GetDetailsShouldReturnViewWithCorrectModel()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var ECAFirst = new ECA
			{
				Description = "Best swimming course",
				Title = "Swimming",
				Id = 1,
				ImageUrl = "some imageUrl",
				MonthlyFee = 9,
			};

			var ECASecond = new ECA
			{

				Description = "Best painting course",
				Title = "Painting",
				Id = 2,
				ImageUrl = "mageUrl",
				MonthlyFee = 8

			};

			data.ECAs.Add(ECAFirst);
			data.ECAs.Add(ECASecond);
			data.SaveChanges();

			var ECAsService = new ECAService(data);

			var ECAsContoller = new ECAsController(data, ECAsService);


			//Act

			var result = ECAsContoller.Details(ECASecond.Id);

			//Assert

			Assert.NotNull(result);
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = viewResult.Model;
			var ECAServiceModel = Assert.IsType<ECAServiceModel>(model);
			Assert.Equal(ECASecond.Title, ECAServiceModel.Title);
			Assert.Equal(ECASecond.Description, ECAServiceModel.Description);
			Assert.Equal(ECASecond.MonthlyFee, ECAServiceModel.MonthlyFee);
			Assert.Equal(ECASecond.ImageUrl, ECAServiceModel.ImageUrl);

		}



	}
}

