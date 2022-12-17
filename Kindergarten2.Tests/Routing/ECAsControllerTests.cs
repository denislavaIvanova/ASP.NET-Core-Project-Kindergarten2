
namespace Kindergarten2.Tests.Routing
{
	using Kindergarten2.Controllers;
	using Kindergarten2.Models.ECAs;
	using MyTested.AspNetCore.Mvc;
	using Xunit;

	public class ECAsControllerTests
	{
		[Fact]
		public void GetAddhouldBeMapped()
			 => MyRouting
				 .Configuration()
				 .ShouldMap("/ECAs/Add")
				 .To<ECAsController>(e => e.Add());

		[Fact]
		public void PostAddShouldBeMapped()
			=> MyRouting
				.Configuration()
				.ShouldMap(request => request
					.WithPath("/ECAs/Add")
					.WithMethod(HttpMethod.Post))
				.To<ECAsController>(p => p.Add(With.Any<AddECAFormModel>()));

		[Fact]
		public void GetAllShouldBeMapped()
		 => MyRouting
			 .Configuration()
			 .ShouldMap("/ECAs/All")
			 .To<ECAsController>(p => p.All(With.Any<AllECAsQueryModel>()));

	}
}
