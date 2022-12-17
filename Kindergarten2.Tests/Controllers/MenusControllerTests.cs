
namespace Kindergarten2.Test.Controllers
{
	using Kindergarten2.Controllers;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Models.Menus;
	using Kindergarten2.Services.Menus;
	using Kindergarten2.Test.Mocks;
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using System.Linq;
	using Xunit;
	public class MenusControllerTests
	{
		[Fact]
		public void AllShouldReturnViewWithCorrectModel()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var menus = new List<MenuServiceModel>();

			var menuFirst = new Menu
			{
				MenuType = "Budget",
				Description = "Best menu",
				Id = 1,
				ImageUrl = "someImageUrl",
				Price = 9
			};

			var menuSecond = new Menu
			{
				MenuType = "Bio",
				Description = "Best Bio menu",
				Id = 2,
				ImageUrl = "someImageUrl",
				Price = 15
			};

			menus.Add(new MenuServiceModel
			{
				MenuType = "Budget",
				Description = "Best menu",
				Id = 1,
				ImageUrl = "someImageUrl",
				Price = 9
			});

			menus.Add(new MenuServiceModel
			{
				MenuType = "Budget",
				Description = "Best menu",
				Id = 1,
				ImageUrl = "someImageUrl",
				Price = 9
			});


			data.Menus.Add(menuFirst);
			data.Menus.Add(menuSecond);
			data.SaveChanges();

			var menuService = new MenuService(data);

			var menusController = new MenusController(data, menuService);

			//Act

			var result = menusController.All(new AllMenusQueryModel
			{
				CurrentPage = 1,
				TotalMenus = 1,
				SearchTerm = "Budget",
				MenuType = "Budget",
				Sorting = default,
				Menus = menus
			});

			//Assert

			Assert.NotNull(result);

			var viewResult = Assert.IsType<ViewResult>(result);

			var model = viewResult.Model;

			var AllMenusQueryModel = Assert.IsType<AllMenusQueryModel>(model);

			Assert.NotNull(AllMenusQueryModel.SearchTerm);
			Assert.NotEqual(" ", AllMenusQueryModel.SearchTerm);
			Assert.Equal("Budget", AllMenusQueryModel.SearchTerm);

			Assert.NotNull(AllMenusQueryModel.MenuType);
			Assert.NotEqual(" ", AllMenusQueryModel.MenuType);
			Assert.Equal("Budget", AllMenusQueryModel.MenuType);

			Assert.Equal(MenuSorting.DateCreated, AllMenusQueryModel.Sorting);

			Assert.Equal(1, AllMenusQueryModel.TotalMenus);
			Assert.Equal(1, AllMenusQueryModel.CurrentPage);
			Assert.Equal(3, AllMenusQueryModel.MenusPerPage);
			Assert.True(menusController.ModelState.IsValid);

		}

		[Fact]
		public void GetAddShouldReturnViewWithCorrectModel()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var menuFirst = new Menu
			{
				MenuType = "Budget",
				Description = "Best menu",
				Id = 1,
				ImageUrl = "someImageUrl",
				Price = 9
			};

			var menuSecond = new Menu
			{
				MenuType = "Bio",
				Description = "Best Bio menu",
				Id = 2,
				ImageUrl = "someImageUrl",
				Price = 15
			};
			data.Menus.Add(menuFirst);
			data.Menus.Add(menuSecond);
			data.SaveChanges();

			var menusService = new MenuService(data);

			var menusContoller = new MenusController(data, menusService);


			//Act

			var result = menusContoller.Add();

			//Assert

			Assert.NotNull(result);
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = viewResult.Model;
			var addmenuFormModel = Assert.IsType<AddMenuFormModel>(model);

		}

		[Fact]
		public void PostAddShouldReturnViewWithCorrectRedirectToAction()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var menusService = new MenuService(data);

			var menusContoller = new MenusController(data, menusService);

			//Act
			var newMenu = new AddMenuFormModel
			{
				MenuType = "Budget",
				Description = "Best menu",
				ImageUrl = "someImageUrl",
				Price = 9
			};

			var result = menusContoller.Add(newMenu);

			var menuToTest = data.Menus.FirstOrDefault(m => m.MenuType == newMenu.MenuType);

			//Assert

			Assert.NotNull(result);
			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);


			Assert.Equal("All", redirectToActionResult.ActionName);

			Assert.True(menusContoller.ModelState.IsValid);
			Assert.Null(redirectToActionResult.ControllerName);
			Assert.Equal(newMenu.MenuType, menuToTest.MenuType);
			Assert.Equal(newMenu.Description, menuToTest.Description);
			Assert.Equal(newMenu.Price, menuToTest.Price);
			Assert.Equal(newMenu.ImageUrl, menuToTest.ImageUrl);

		}

		[Fact]
		public void GetEditShouldReturnViewWithCorrectModel()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var menuFirst = new Menu
			{
				MenuType = "Budget",
				Description = "Best menu",
				Id = 1,
				ImageUrl = "someImageUrl",
				Price = 9
			};

			var menuSecond = new Menu
			{
				MenuType = "Bio",
				Description = "Best Bio menu",
				Id = 2,
				ImageUrl = "someImageUrl",
				Price = 15
			};
			data.Menus.Add(menuFirst);
			data.Menus.Add(menuSecond);
			data.SaveChanges();

			var menusService = new MenuService(data);
			var menusContoller = new MenusController(data, menusService);

			//Act

			var result = menusContoller.Edit(menuSecond.Id);

			//Assert

			Assert.NotNull(result);
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = viewResult.Model;
			var menuServiceModel = Assert.IsType<MenuServiceModel>(model);
			Assert.Equal(menuSecond.MenuType, menuServiceModel.MenuType);
			Assert.Equal(menuSecond.Description, menuServiceModel.Description);
			Assert.Equal(menuSecond.Price, menuServiceModel.Price);
			Assert.Equal(menuSecond.ImageUrl, menuServiceModel.ImageUrl);

		}

		[Fact]
		public void PostEditShouldReturnViewWithCorrectRedirectToAction()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var menusService = new MenuService(data);

			var menusContoller = new MenusController(data, menusService);

			var menuFirst = new Menu
			{
				MenuType = "Budget",
				Description = "Best menu",
				Id = 1,
				ImageUrl = "someImageUrl",
				Price = 9
			};

			var menuSecond = new Menu
			{
				MenuType = "Bio",
				Description = "Best Bio menu",
				Id = 2,
				ImageUrl = "someImageUrl",
				Price = 15
			};

			data.Menus.Add(menuFirst);
			data.Menus.Add(menuSecond);
			data.SaveChanges();

			//Act
			var editedMenu = new MenuServiceModel
			{
				MenuType = "Bio mio",
				Description = "Best menu",
				Id = 2,
				ImageUrl = "ImageUrl",
				Price = 150
			};

			var result = menusContoller.Edit(menuSecond.Id, editedMenu);

			var menuToTest = data.Menus.FirstOrDefault(e => e.Id == editedMenu.Id);

			//Assert

			Assert.NotNull(result);
			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);


			Assert.Equal("All", redirectToActionResult.ActionName);

			Assert.True(menusContoller.ModelState.IsValid);
			Assert.Null(redirectToActionResult.ControllerName);
			Assert.Equal(editedMenu.MenuType, menuToTest.MenuType);
			Assert.Equal(editedMenu.Description, menuToTest.Description);
			Assert.Equal(editedMenu.ImageUrl, menuToTest.ImageUrl);
			Assert.Equal(editedMenu.Price, menuToTest.Price);



		}

		[Fact]
		public void PostDeleteShouldRedirectToAllAndRemoveEntryFromDb()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var menusService = new MenuService(data);

			var menusContoller = new MenusController(data, menusService);

			var menuFirst = new Menu
			{
				MenuType = "Budget",
				Description = "Best menu",
				Id = 1,
				ImageUrl = "someImageUrl",
				Price = 9
			};

			var menuSecond = new Menu
			{
				MenuType = "Bio",
				Description = "Best Bio menu",
				Id = 2,
				ImageUrl = "someImageUrl",
				Price = 15
			};

			data.Menus.Add(menuFirst);
			data.Menus.Add(menuSecond);
			data.SaveChanges();

			//Act
			var result = menusContoller.Delete(menuFirst.Id);

			var menuToTest = data.Menus.FirstOrDefault(t => t.Id == menuFirst.Id);

			//Assert

			Assert.NotNull(result);
			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);


			Assert.Equal("All", redirectToActionResult.ActionName);

			Assert.Null(redirectToActionResult.ControllerName);
			Assert.Null(menuToTest);
			Assert.Equal(1, data.Menus.Count());

		}

		[Fact]
		public void GetDetailsShouldReturnViewWithCorrectModel()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var menuFirst = new Menu
			{
				MenuType = "Budget",
				Description = "Best menu",
				Id = 1,
				ImageUrl = "someImageUrl",
				Price = 9
			};

			var menuSecond = new Menu
			{
				MenuType = "Bio",
				Description = "Best Bio menu",
				Id = 2,
				ImageUrl = "someImageUrl",
				Price = 15
			};
			data.Menus.Add(menuFirst);
			data.Menus.Add(menuSecond);
			data.SaveChanges();

			var menusService = new MenuService(data);
			var menusContoller = new MenusController(data, menusService);

			//Act

			var result = menusContoller.Details(menuSecond.Id);

			//Assert

			Assert.NotNull(result);
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = viewResult.Model;
			var menuServiceModel = Assert.IsType<MenuServiceModel>(model);
			Assert.Equal(menuSecond.MenuType, menuServiceModel.MenuType);
			Assert.Equal(menuSecond.Description, menuServiceModel.Description);
			Assert.Equal(menuSecond.Price, menuServiceModel.Price);
			Assert.Equal(menuSecond.ImageUrl, menuServiceModel.ImageUrl);

		}

	}
}

