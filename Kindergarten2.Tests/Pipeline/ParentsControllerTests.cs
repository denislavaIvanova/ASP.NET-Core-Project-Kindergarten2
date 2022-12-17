

namespace Kindergarten2.Test.Pipeline
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
		public void GetBecomeShouldBeForAuthorizedUsersAndReturnView()
			=> MyPipeline
				.Configuration()
				.ShouldMap(request => request
					.WithPath("/Parents/Become")
					.WithUser())
				.To<ParentsController>(c => c.Become())
				.Which()
				.ShouldHave()
				.ActionAttributes(attributes => attributes
					.RestrictingForAuthorizedRequests())
				.AndAlso()
				.ShouldReturn()
				.View();

		[Theory]
		[InlineData("ParentFirstName", "ParentLastName", "ParentPhone")]
		public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
			string parentFirstName,
			string parentLastName,
			string phoneNumber)
			=> MyPipeline
				.Configuration()
				.ShouldMap(request => request
					.WithPath("/Parents/Become")
					.WithMethod(HttpMethod.Post)
					.WithFormFields(new
					{
						FirstName = parentFirstName,
						LastName = parentLastName,
						PhoneNumber = phoneNumber
					})
					.WithUser()
					.WithAntiForgeryToken())
				.To<ParentsController>(c => c.Become(new RegisterAsParentFormModel
				{
					FirstName = parentFirstName,
					LastName = parentLastName,
					PhoneNumber = phoneNumber
				}))
				.Which()
				.ShouldHave()
				.ActionAttributes(attributes => attributes
					.RestrictingForHttpMethod(HttpMethod.Post)
					.RestrictingForAuthorizedRequests())
				.ValidModelState()
				.Data(data => data
					.WithSet<Parent>(p => p
						.Any(p =>
							p.FirstName == parentFirstName &&
							p.LastName == parentLastName &&
						p.PhoneNumber == phoneNumber &&
							p.UserId == TestUser.Identifier)))
				.AndAlso()
				.ShouldReturn()
				.Redirect(redirect => redirect
					.To<ChildsController>(c => c.Add(With.Any<ChildFormModel>())));
	}
}
