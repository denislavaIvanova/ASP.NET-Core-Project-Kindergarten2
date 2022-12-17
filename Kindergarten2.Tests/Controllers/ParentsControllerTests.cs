

namespace Kindergarten2.Test.Controllers
{
	using Kindergarten2.Controllers;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Models.Parents;
	using Kindergarten2.Services.Childs;
	using MyTested.AspNetCore.Mvc;
	using System.Linq;
	using Xunit;
	public class ParentsControllerTests
	{
		[Fact]

		public void BecomeShouldBeForAuthorizedUsersAndReturnView()
			=> MyController<ParentsController>
				.Instance()
				.Calling(c => c.Become())
				.ShouldHave()
				.ActionAttributes(attributes => attributes
					.RestrictingForAuthorizedRequests())
				.AndAlso()
				.ShouldReturn()
				.View();

		[Theory]
		[InlineData("ParentFirstName", "ParentLastName", "ParentPhone")]
		public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(string parentFirstName,
			string parentLastName,
			string phoneNumber)
			=> MyController<ParentsController>
			.Instance(controller => controller
			.WithUser())
			.Calling(p => p.Become(new RegisterAsParentFormModel
			{
				FirstName = parentFirstName,
				LastName = parentLastName,
				PhoneNumber = phoneNumber
			}))
			.ShouldHave()
			.ActionAttributes(attributes => attributes
			.RestrictingForHttpMethod(HttpMethod.Post)
			.RestrictingForAuthorizedRequests())
			.ValidModelState()
			.Data(data => data
					.WithSet<Parent>(parents => parents
						.Any(p =>
							p.FirstName == parentFirstName &&
							p.LastName == parentLastName &&
							p.UserId == TestUser.Identifier)))
				.AndAlso()
				.ShouldReturn()
				.Redirect(redirect => redirect
					.To<ChildsController>(c => c.Add(With.Any<ChildFormModel>())));


	}
}
