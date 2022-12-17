

namespace Kindergarten2.Tests.Routing
{
	using Kindergarten2.Controllers;
	using Kindergarten2.Models.Menus;
	using MyTested.AspNetCore.Mvc;
	using Xunit;
	public class MenusControllerTests
	{
		[Fact]
		public void GetAddhouldBeMapped()
			 => MyRouting
				 .Configuration()
				 .ShouldMap("/Menus/Add")
				 .To<MenusController>(m => m.Add());

		[Fact]
		public void PostAddShouldBeMapped()
			=> MyRouting
				.Configuration()
				.ShouldMap(request => request
					.WithPath("/Menus/Add")
					.WithMethod(HttpMethod.Post))
				.To<MenusController>(m => m.Add(With.Any<AddMenuFormModel>()));

		[Fact]
		public void GetAllShouldBeMapped()
		 => MyRouting
			 .Configuration()
			 .ShouldMap("/Menus/All")
			 .To<MenusController>(m => m.All(With.Any<AllMenusQueryModel>()));
	}
}
