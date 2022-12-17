

namespace Kindergarten2.Tests.Routing
{
	using Kindergarten2.Controllers;
	using Kindergarten2.Models.Trips;
	using MyTested.AspNetCore.Mvc;
	using Xunit;
	public class TripsControllerTests
	{
		[Fact]
		public void GetAddhouldBeMapped()
			 => MyRouting
				 .Configuration()
				 .ShouldMap("/Trips/Add")
				 .To<TripsController>(p => p.Add());

		[Fact]
		public void PostAddShouldBeMapped()
			=> MyRouting
				.Configuration()
				.ShouldMap(request => request
					.WithPath("/Trips/Add")
					.WithMethod(HttpMethod.Post))
				.To<TripsController>(p => p.Add(With.Any<AddTripFormModel>()));

		[Fact]
		public void GetAllShouldBeMapped()
		 => MyRouting
			 .Configuration()
			 .ShouldMap("/Trips/All")
			 .To<TripsController>(p => p.All(With.Any<AllTripsQueryModel>()));

	}
}
