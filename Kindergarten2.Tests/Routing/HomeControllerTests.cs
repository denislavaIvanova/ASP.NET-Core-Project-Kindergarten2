

namespace Kindergarten2.Test.Routing
{
	using Kindergarten2.Controllers;
	using MyTested.AspNetCore.Mvc;
	using Xunit;

	public class HomeControllerTests
	{
		[Fact]
		public void IndexRouteShouldBeMapped()
		   => MyRouting
			   .Configuration()
			   .ShouldMap("/")
			   .To<HomeController>(c => c.Index());

		[Fact]
		public void ErrorRouteShouldBeMapped()
			=> MyRouting
				.Configuration()
				.ShouldMap("/Home/Error")
				.To<HomeController>(c => c.Error());
	}
}
