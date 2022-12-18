

namespace Kindergarten2.Tests.Controllers
{
	using Kindergarten2.Controllers;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Models.Teachers;
	using Kindergarten2.Services.Teachers;
	using Kindergarten2.Test.Mocks;
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using System.Linq;
	using Xunit;
	public class TeachersControllerTests
	{
		[Fact]
		public void AllShouldReturnViewWithCorrectModel()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var teachers = new List<TeacherServiceModel>();
			var group = new Group
			{
				Id = 1,
				Name = "Daisy",
				ChildrenCount = 9
			};

			data.Groups.Add(group);
			data.SaveChanges();

			var teacherFirst = new Teacher
			{
				FirstName = "Poly",
				LastName = "Parker",
				Experience = 9,
				Id = 1,
				GroupId = 1,
				Specialization = "Music",
				ImageUrl = "someUrl",
				Introduction = "I am the best teacher"
			};

			var teacherSecond = new Teacher
			{
				FirstName = "Moly",
				LastName = "Potter",
				Experience = 12,
				Id = 2,
				GroupId = 1,
				Specialization = "Theathre",
				ImageUrl = "someUrl",
				Introduction = "I am the best artist"
			};

			teachers.Add(new TeacherServiceModel
			{
				FirstName = "Poly",
				LastName = "Parker",
				Experience = 9,
				Id = 1,
				Group = "Daisy",
				Specialization = "Music",
				ImageUrl = "someUrl",
				Introduction = "I am the best teacher",
			});

			teachers.Add(new TeacherServiceModel
			{
				FirstName = "Moly",
				LastName = "Potter",
				Experience = 12,
				Id = 2,
				Group = "Daisy",
				Specialization = "Theathre",
				ImageUrl = "someUrl",
				Introduction = "I am the best artist"
			});


			data.Teachers.Add(teacherFirst);
			data.Teachers.Add(teacherSecond);
			data.SaveChanges();

			var teacherService = new TeacherService(data);

			var teachersController = new TeachersController(data, teacherService);

			//Act

			var result = teachersController.All(new AllTeachersQueryModel
			{
				CurrentPage = 1,
				TotalTeachers = 1,
				SearchTerm = "Potter",
				Specialization = "Theathre",
				Sorting = default,
				Teachers = teachers
			});

			//Assert

			Assert.NotNull(result);

			var viewResult = Assert.IsType<ViewResult>(result);

			var model = viewResult.Model;

			var AllTeachersQueryModel = Assert.IsType<AllTeachersQueryModel>(model);

			Assert.NotNull(AllTeachersQueryModel.SearchTerm);
			Assert.NotEqual(" ", AllTeachersQueryModel.SearchTerm);
			Assert.Equal("Potter", AllTeachersQueryModel.SearchTerm);

			Assert.NotNull(AllTeachersQueryModel.Specialization);
			Assert.NotEqual(" ", AllTeachersQueryModel.Specialization);
			Assert.Equal("Theathre", AllTeachersQueryModel.Specialization);

			Assert.Equal(TeacherSorting.DateCreated, AllTeachersQueryModel.Sorting);

			Assert.Equal(1, AllTeachersQueryModel.TotalTeachers);
			Assert.Equal(1, AllTeachersQueryModel.CurrentPage);
			Assert.Equal(3, AllTeachersQueryModel.TeachersPerPage);
			Assert.True(teachersController.ModelState.IsValid);

		}

		[Fact]
		public void GetAddShouldReturnViewWithCorrectModel()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var group = new Group
			{
				Id = 1,
				Name = "Daisy",
				ChildrenCount = 9
			};

			data.Groups.Add(group);
			data.SaveChanges();

			var teacherFirst = new Teacher
			{
				FirstName = "Poly",
				LastName = "Parker",
				Experience = 9,
				Id = 1,
				GroupId = 1,
				Specialization = "Music",
				ImageUrl = "someUrl",
				Introduction = "I am the best teacher"
			};

			var teacherSecond = new Teacher
			{
				FirstName = "Moly",
				LastName = "Potter",
				Experience = 12,
				Id = 2,
				GroupId = 1,
				Specialization = "Theathre",
				ImageUrl = "someUrl",
				Introduction = "I am the best artist"
			};

			data.Teachers.Add(teacherFirst);
			data.Teachers.Add(teacherSecond);
			data.SaveChanges();

			var teachersService = new TeacherService(data);
			var teachersContoller = new TeachersController(data, teachersService);

			//Act

			var result = teachersContoller.Add();

			//Assert

			Assert.NotNull(result);
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = viewResult.Model;
			var addTeacherFormModel = Assert.IsType<TeacherFormModel>(model);

		}

		[Fact]
		public void PostAddShouldReturnViewWithCorrectRedirectToAction()
		{
			//Arrange
			var data = DatabaseMock.Instance;
			var TeachersService = new TeacherService(data);
			var teachersContoller = new TeachersController(data, TeachersService);

			var group = new Group
			{
				Id = 1,
				Name = "Daisy",
				ChildrenCount = 9
			};

			data.Groups.Add(group);
			data.SaveChanges();

			//Act

			var newTeacher = new TeacherFormModel
			{
				FirstName = "Moly",
				LastName = "Potter",
				Experience = 12,
				GroupId = 1,
				Specialization = "Theathre",
				ImageUrl = "someUrl",
				Introduction = "I am the best artist"
			};

			var result = teachersContoller.Add(newTeacher);

			var teacherToTest = data.Teachers.FirstOrDefault(t => t.FirstName == newTeacher.FirstName);

			//Assert

			Assert.NotNull(result);
			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("All", redirectToActionResult.ActionName);

			Assert.True(teachersContoller.ModelState.IsValid);
			Assert.Null(redirectToActionResult.ControllerName);
			Assert.Equal(newTeacher.FirstName, teacherToTest.FirstName);
			Assert.Equal(newTeacher.LastName, teacherToTest.LastName);
			Assert.Equal(newTeacher.Specialization, teacherToTest.Specialization);
			Assert.Equal(newTeacher.Experience, teacherToTest.Experience);
			Assert.Equal(newTeacher.ImageUrl, teacherToTest.ImageUrl);
			Assert.Equal(newTeacher.Introduction, teacherToTest.Introduction);
			Assert.Equal(newTeacher.GroupId, teacherToTest.GroupId);
			Assert.Equal(1, data.Teachers.Count());


		}

		[Fact]
		public void GetEditShouldReturnViewWithCorrectModel()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var group = new Group
			{
				Id = 1,
				Name = "Daisy",
				ChildrenCount = 9
			};

			data.Groups.Add(group);
			data.SaveChanges();

			var teacherFirst = new Teacher
			{
				FirstName = "Poly",
				LastName = "Parker",
				Experience = 9,
				Id = 1,
				GroupId = 1,
				Specialization = "Music",
				ImageUrl = "someUrl",
				Introduction = "I am the best teacher"
			};

			var teacherSecond = new Teacher
			{
				FirstName = "Moly",
				LastName = "Potter",
				Experience = 12,
				Id = 2,
				GroupId = 1,
				Specialization = "Theathre",
				ImageUrl = "someUrl",
				Introduction = "I am the best artist"
			};

			data.Teachers.Add(teacherFirst);
			data.Teachers.Add(teacherSecond);
			data.SaveChanges();

			var teachersService = new TeacherService(data);
			var teachersContoller = new TeachersController(data, teachersService);

			//Act

			var result = teachersContoller.Edit(teacherSecond.Id);

			//Assert

			Assert.NotNull(result);
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = viewResult.Model;
			var teacherFormModel = Assert.IsType<TeacherFormModel>(model);
			Assert.Equal(teacherSecond.FirstName, teacherFormModel.FirstName);
			Assert.Equal(teacherSecond.LastName, teacherFormModel.LastName);
			Assert.Equal(teacherSecond.Specialization, teacherFormModel.Specialization);
			Assert.Equal(teacherSecond.Introduction, teacherFormModel.Introduction);
			Assert.Equal(teacherSecond.ImageUrl, teacherFormModel.ImageUrl);
			Assert.Equal(teacherSecond.Experience, teacherFormModel.Experience);
			Assert.Equal(teacherSecond.GroupId, teacherFormModel.GroupId);

		}

		[Fact]
		public void PostEditShouldReturnViewWithCorrectRedirectToAction()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var groupOne = new Group
			{
				Id = 1,
				Name = "Daisy",
				ChildrenCount = 9
			};

			var groupTwo = new Group
			{
				Id = 2,
				Name = "Sun",
				ChildrenCount = 10
			};


			data.Groups.Add(groupOne);
			data.SaveChanges();

			data.Groups.Add(groupTwo);
			data.SaveChanges();

			var teacherFirst = new Teacher
			{
				FirstName = "Poly",
				LastName = "Parker",
				Experience = 9,
				Id = 1,
				GroupId = 1,
				Specialization = "Music",
				ImageUrl = "someUrl",
				Introduction = "I am the best teacher"
			};

			var teacherSecond = new Teacher
			{
				FirstName = "Moly",
				LastName = "Potter",
				Experience = 12,
				Id = 2,
				GroupId = 1,
				Specialization = "Theathre",
				ImageUrl = "someUrl",
				Introduction = "I am the best artist"
			};

			data.Teachers.Add(teacherFirst);
			data.Teachers.Add(teacherSecond);
			data.SaveChanges();

			var teachersService = new TeacherService(data);
			var teachersContoller = new TeachersController(data, teachersService);

			//Act
			var editedTeacher = new TeacherFormModel
			{
				FirstName = "Soly",
				LastName = "Tolly",
				Experience = 1,
				Specialization = "Music",
				ImageUrl = "someUrl",
				Introduction = "I am the best musician",
				GroupId = 2
			};

			var result = teachersContoller.Edit(teacherSecond.Id, editedTeacher);

			var teacherToTest = data.Teachers.FirstOrDefault(t => t.Id == teacherSecond.Id);

			//Assert

			Assert.NotNull(result);
			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);


			Assert.Equal("All", redirectToActionResult.ActionName);

			Assert.True(teachersContoller.ModelState.IsValid);
			Assert.Null(redirectToActionResult.ControllerName);
			Assert.Equal(editedTeacher.FirstName, teacherToTest.FirstName);
			Assert.Equal(editedTeacher.LastName, teacherToTest.LastName);
			Assert.Equal(editedTeacher.ImageUrl, teacherToTest.ImageUrl);
			Assert.Equal(editedTeacher.Specialization, teacherToTest.Specialization);
			Assert.Equal(editedTeacher.Introduction, teacherToTest.Introduction);
			Assert.Equal(editedTeacher.Experience, teacherToTest.Experience);
			Assert.Equal(editedTeacher.GroupId, teacherToTest.GroupId);

		}

		//[Fact]
		//public void PostDeleteShouldRedirectToAllAndRemoveEntryFromDb()
		//{
		//	//Arrange
		//	var data = DatabaseMock.Instance;

		//	var groupOne = new Group
		//	{
		//		Id = 1,
		//		Name = "Daisy",
		//		ChildrenCount = 9
		//	};

		//	var groupTwo = new Group
		//	{
		//		Id = 2,
		//		Name = "Sun",
		//		ChildrenCount = 10
		//	};


		//	data.Groups.Add(groupOne);
		//	data.SaveChanges();

		//	data.Groups.Add(groupTwo);
		//	data.SaveChanges();

		//	var teacherFirst = new Teacher
		//	{
		//		FirstName = "Poly",
		//		LastName = "Parker",
		//		Experience = 9,
		//		Id = 1,
		//		GroupId = 1,
		//		Specialization = "Music",
		//		ImageUrl = "someUrl",
		//		Introduction = "I am the best teacher"
		//	};

		//	var teacherSecond = new Teacher
		//	{
		//		FirstName = "Moly",
		//		LastName = "Potter",
		//		Experience = 12,
		//		Id = 2,
		//		GroupId = 1,
		//		Specialization = "Theathre",
		//		ImageUrl = "someUrl",
		//		Introduction = "I am the best artist"
		//	};

		//	data.Teachers.Add(teacherFirst);
		//	data.Teachers.Add(teacherSecond);
		//	data.SaveChanges();

		//	var teachersService = new TeacherService(data);
		//	var teachersContoller = new TeachersController(data, teachersService);

		//	//Act
		//	var result = teachersContoller.Delete(teacherFirst.Id);

		//	var teacherToTest = data.Teachers.FirstOrDefault(t => t.Id == teacherFirst.Id);

		//	//Assert

		//	Assert.NotNull(result);
		//	var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

		//	Assert.Equal("All", redirectToActionResult.ActionName);
		//	Assert.Null(redirectToActionResult.ControllerName);
		//	Assert.Null(teacherToTest);
		//	Assert.Equal(1, data.Teachers.Count());

		//}

		[Fact]
		public void GetDetailsShouldReturnViewWithCorrectModel()
		{
			//Arrange
			var data = DatabaseMock.Instance;

			var group = new Group
			{
				Id = 1,
				Name = "Daisy",
				ChildrenCount = 9
			};

			data.Groups.Add(group);
			data.SaveChanges();

			var teacherFirst = new Teacher
			{
				FirstName = "Poly",
				LastName = "Parker",
				Experience = 9,
				Id = 1,
				GroupId = 1,
				Specialization = "Music",
				ImageUrl = "someUrl",
				Introduction = "I am the best teacher"
			};

			var teacherSecond = new Teacher
			{
				FirstName = "Moly",
				LastName = "Potter",
				Experience = 12,
				Id = 2,
				GroupId = 1,
				Specialization = "Theathre",
				ImageUrl = "someUrl",
				Introduction = "I am the best artist"
			};

			data.Teachers.Add(teacherFirst);
			data.Teachers.Add(teacherSecond);
			data.SaveChanges();

			var teachersService = new TeacherService(data);
			var teachersContoller = new TeachersController(data, teachersService);

			//Act

			var result = teachersContoller.Details(teacherSecond.Id);

			//Assert

			Assert.NotNull(result);
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = viewResult.Model;
			var teacherModel = Assert.IsType<TeacherDetailsServiceModel>(model);
			Assert.Equal(teacherSecond.FirstName, teacherModel.FirstName);
			Assert.Equal(teacherSecond.LastName, teacherModel.LastName);
			Assert.Equal(teacherSecond.Specialization, teacherModel.Specialization);
			Assert.Equal(teacherSecond.Introduction, teacherModel.Introduction);
			Assert.Equal(teacherSecond.ImageUrl, teacherModel.ImageUrl);
			Assert.Equal(teacherSecond.Experience, teacherModel.Experience);
			Assert.Equal(teacherSecond.GroupId, teacherModel.GroupId);

		}

	}
}
