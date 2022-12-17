namespace Kindergarten2.Test.Controllers
{
	using Kindergarten2.Controllers;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Models.Home;
	using Kindergarten2.Services.Teachers;
	using MyTested.AspNetCore.Mvc;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Xunit;

	public class HomeControllerTests
	{
		[Fact]
		public void IndexShouldReturnCorrectViewWithModel()
		  => MyController<HomeController>
			  .Instance(controller => controller
			  .WithData(GetTeachers))
				.Calling(t => t.Index())
			.ShouldReturn()
			.View(view => view
			.WithModelOfType<IndexViewModel>()
			.Passing(m => m.Teachers.Count() == 3));

		[Fact]
		public void IndexShouldReturnCorrectViewWithModelAndData()
			=> MyController<HomeController>
				.Instance(controller => controller
					.WithData(GetTeachers))
				.Calling(c => c.Index())
				.ShouldHave()
				.MemoryCache(cache => cache
					.ContainingEntry(entry => entry
						.WithKey("LatestTeachersCacheKey")
						.WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(10))
						.WithValueOfType<List<LatestTeacherServiceModel>>()))
				.AndAlso()
				.ShouldReturn()
				.View(view => view
					.WithModelOfType<IndexViewModel>()
					.Passing(model => model.Teachers.Count()));


		[Fact]
		public void ErrorShouldReturnView()
			=> MyController<HomeController>
				.Instance()
				.Calling(c => c.Error())
				.ShouldReturn()
				.View();

		public static IEnumerable<Teacher> TenTeachers
		   => Enumerable.Range(0, 10).Select(i => new Teacher());


		//[Fact]
		//public void IndexShouldReturnViewWithCorrectModel()
		//{
		//	//Arrange
		//	var data = DatabaseMock.Instance;


		//	var teachers = Enumerable.Range(0, 10).Select(i => new Teacher());

		//	data.Teachers.AddRange(teachers);

		//	data.Children.Add(new Child());
		//	data.Groups.Add(new Group());


		//	data.SaveChanges();

		//	var teacherService = new TeacherService(data);

		//	var statisticsService = new StatisticsService(data);

		//	var memoryCache=cac

		//	var homeController = new HomeController(statisticsService, teacherService, null);

		//	//Act

		//	var result = homeController.Index();

		//	//Assert

		//	Assert.NotNull(result);

		//	var viewResult = Assert.IsType<ViewResult>(result);

		//	var model = viewResult.Model;

		//	var indexViewModel = Assert.IsType<IndexViewModel>(model);

		//	Assert.Equal(3, indexViewModel.Teachers.Count);
		//	Assert.Equal(10, indexViewModel.TotalTeachers);
		//	Assert.Equal(1, indexViewModel.TotalChildren);
		//	Assert.Equal(1, indexViewModel.TotalGroups);



		//}

		//[Fact]
		//public void ErrorShouldReturnView()
		//{
		//	//Arrange
		//	var homeController = new HomeController(null, null, null);

		//	//Act
		//	var result = homeController.Error();

		//	//Assert

		//	Assert.NotNull(result);
		//	Assert.IsType<ViewResult>(result);
		//}
		public static IEnumerable<Teacher> GetTeachers
			=> Enumerable.Range(0, 10).Select(i => new Teacher());
	}
}
