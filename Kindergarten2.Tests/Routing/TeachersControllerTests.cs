

namespace Kindergarten2.Tests.Routing
{
	using Kindergarten2.Controllers;
	using Kindergarten2.Models.Teachers;
	using MyTested.AspNetCore.Mvc;
	using Xunit;

	public class TeachersControllerTests
	{
		[Fact]
		public void GetAddhouldBeMapped()
			 => MyRouting
				 .Configuration()
				 .ShouldMap("/Teachers/Add")
				 .To<TeachersController>(t => t.Add());

		[Fact]
		public void PostAddShouldBeMapped()
			=> MyRouting
				.Configuration()
				.ShouldMap(request => request
					.WithPath("/Teachers/Add")
					.WithMethod(HttpMethod.Post))
				.To<TeachersController>(p => p.Add(With.Any<TeacherFormModel>()));

		[Fact]
		public void GetAllShouldBeMapped()
		 => MyRouting
			 .Configuration()
			 .ShouldMap("/Teachers/All")
			 .To<TeachersController>(p => p.All(With.Any<AllTeachersQueryModel>()));
	}
}
