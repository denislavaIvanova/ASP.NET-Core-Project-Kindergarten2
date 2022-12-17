

namespace Kindergarten2.Test.Routing
{
	using Kindergarten2.Controllers;
	using Kindergarten2.Models.Parents;
	using MyTested.AspNetCore.Mvc;
	using Xunit;

	public class ParentsControllerTests
	{
		[Fact]
		public void GetBecomeShouldBeMapped()
			 => MyRouting
				 .Configuration()
				 .ShouldMap("/Parents/Become")
				 .To<ParentsController>(c => c.Become());

		[Fact]
		public void PostBecomeShouldBeMapped()
			=> MyRouting
				.Configuration()
				.ShouldMap(request => request
					.WithPath("/Parents/Become")
					.WithMethod(HttpMethod.Post))
				.To<ParentsController>(c => c.Become(With.Any<RegisterAsParentFormModel>()));
	}
}
